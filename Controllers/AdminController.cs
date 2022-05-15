using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pdi_Car_Rent.Areas.Identity.Data;
using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Pdi_Car_RentUserContext _context;

        public AdminController(UserManager<IdentityUser> userManager, Pdi_Car_RentUserContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult AdminPanel(string warning = "")
        {
            ViewBag.Players = _context.Users.ToList();
            ViewBag.PlayerRoles = _context.Roles.ToList();
            ViewBag.Warning = warning;
            return View();
        }
        [HttpPost]
        public IActionResult AdminPanel(MainAdminModel userRole)
        {
            bool flag = true;
            foreach (var item in _context.UserRoles)
            {
                if (item.UserId == userRole.RoleModel.User.Id &&
                    item.RoleId == userRole.RoleModel.Role.Id.ToString())
                    return RedirectToAction(nameof(AdminPanel),
                        new { warning = "Ten użytkownik posiada już w tę role" });
            }
            if (flag == true)
            {
                var result = _context.UserRoles.FirstOrDefault(x => x.UserId == userRole.RoleModel.User.Id);
                if (result != null)
                {
                    _context.UserRoles.Remove(result);
                    _context.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = userRole.RoleModel.User.Id,
                        RoleId = userRole.RoleModel.Role.Id.ToString()
                    });

                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(AdminPanel));
        }


        [HttpGet]
        public IActionResult AdminPanelRentPlace(string warning = "")
        {
            var workerId = _context.Roles.First(x => x.Name == "Pracownik").Id;
            var Employees = _context.UserRoles.Where(x => x.RoleId == workerId).ToList();
            ViewBag.Employees = _context.Users.Where(x=>Employees.All(y=>y.UserId == x.Id)).ToList();
            ViewBag.Warning = warning;
            return View();
        }
        [HttpPost]
        public IActionResult AdminPanelRentPlace(MainAdminModel userRole)
        {
            bool flag = true;
            foreach (var item in _context.UserRoles)
            {
                if (item.UserId == userRole.RoleModel.User.Id &&
                    item.RoleId == userRole.RoleModel.Role.Id.ToString())
                    return RedirectToAction(nameof(AdminPanel),
                        new { warning = "Ten użytkownik posiada już w tę role" });
            }
            if (flag == true)
            {
                var result = _context.UserRoles.FirstOrDefault(x => x.UserId == userRole.RoleModel.User.Id);
                if (result != null)
                {
                    _context.UserRoles.Remove(result);
                    _context.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = userRole.RoleModel.User.Id,
                        RoleId = userRole.RoleModel.Role.Id.ToString()
                    });

                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(AdminPanel));
        }
    }
}
