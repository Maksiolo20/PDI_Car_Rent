using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pdi_Car_Rent.Areas.Identity.Data;
using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Pdi_Car_RentUserContext _userContext;
        private readonly DatabaseContext _dbContext;

        public AdminController(UserManager<IdentityUser> userManager, Pdi_Car_RentUserContext userContext, DatabaseContext dbContext)
        {
            _userManager = userManager;
            _userContext = userContext;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult AdminPanel()
        {
            ViewBag.Users = _userContext.Users.ToList();
            ViewBag.UserRoles = _userContext.Roles.ToList();
            ViewBag.RentPlace = _dbContext.CarRentPlace.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AdminPanelRentPlace(MainAdminModel model)
        {
            if (model.UserId != null && model.UserRoleId != null)
            {
                var result = _userContext.UserRoles.FirstOrDefault(x => x.UserId == model.UserId);
                if (result != null)
                {
                    _userContext.UserRoles.Remove(result);
                    _userContext.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = model.UserId,
                        RoleId = model.UserRoleId
                    });

                    _userContext.SaveChanges();
                }
            }
            if (model.RentPlaceId != 0 && model.WorkerId != null)
            {
                var result = _dbContext.CarRentPlace.FirstOrDefault(x => x.Id == model.RentPlaceId);
                if (result != null)
                {
                    _dbContext.CarRentPlace
                        .First(x=>x.Id==model.RentPlaceId).WorkerId=int.Parse(model.WorkerId);
                    _dbContext.SaveChanges();
                }
            }
            //var user = _userContext.UserRoles.First(x => x.UserId == role.RoleModel.User.Id);
            //var role2 = _userContext.Roles.First(x => x.Id == user.RoleId);
            return RedirectToAction(nameof(AdminPanel));
        }
    }
}
