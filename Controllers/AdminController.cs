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
        public IActionResult AdminPanel(string warning = "")
        {
            ViewBag.Players = _userContext.Users.ToList();
            ViewBag.PlayerRoles = _userContext.Roles.ToList();
            ViewBag.Warning = warning;
            return View();
        }
        [HttpPost]
        public IActionResult AdminPanel(MainAdminModel userRole)
        {
            bool flag = true;
            foreach (var item in _userContext.UserRoles)
            {
                if (item.UserId == userRole.RoleModel.User.Id &&
                    item.RoleId == userRole.RoleModel.Role.Id.ToString())
                    return RedirectToAction(nameof(AdminPanel),
                        new { warning = "Ten użytkownik posiada już w tę role" });
            }
            if (flag == true)
            {
                var result = _userContext.UserRoles.FirstOrDefault(x => x.UserId == userRole.RoleModel.User.Id);
                if (result != null)
                {
                    _userContext.UserRoles.Remove(result);
                    _userContext.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = userRole.RoleModel.User.Id,
                        RoleId = userRole.RoleModel.Role.Id.ToString()
                    });

                    _userContext.SaveChanges();
                }
            }
            return RedirectToAction(nameof(AdminPanel));
        }


        [HttpGet]
        public IActionResult AdminPanelRentPlace(string warning = "")
        {
            var workerId = _userContext.Roles.First(x => x.Name == "Pracownik").Id;
            var Employees = _userContext.UserRoles.Where(x => x.RoleId == workerId).ToList();
            ViewBag.Employees = _userContext.Users.Where(x=>Employees.All(y=>y.UserId == x.Id)).ToList();
            var RentPlace = 
            ViewBag.Warning = warning;
            return View();
        }
        [HttpPost]
        public IActionResult AdminPanelRentPlace(MainAdminModel userRole)
        {
            bool flag = true;
            foreach (var item in _userContext.UserRoles)
            {
                if (item.UserId == userRole.RoleModel.User.Id &&
                    item.RoleId == userRole.RoleModel.Role.Id.ToString())
                    return RedirectToAction(nameof(AdminPanel),
                        new { warning = "Ten użytkownik posiada już w tę role" });
            }
            if (flag == true)
            {
                var result = _userContext.UserRoles.FirstOrDefault(x => x.UserId == userRole.RoleModel.User.Id);
                if (result != null)
                {
                    _userContext.UserRoles.Remove(result);
                    _userContext.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = userRole.RoleModel.User.Id,
                        RoleId = userRole.RoleModel.Role.Id.ToString()
                    });

                    _userContext.SaveChanges();
                }
            }
            return RedirectToAction(nameof(AdminPanel));
        }
    }
}
