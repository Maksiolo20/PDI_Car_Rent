﻿using Microsoft.AspNetCore.Identity;
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
        public IActionResult AdminPanel(AddingRoleModel userRole)
        {
            bool flag = true;
            foreach (var item in _context.UserRoles)
            {
                if (item.UserId == userRole.User.Id &&
                    item.RoleId == userRole.Role.Id.ToString())
                    return RedirectToAction(nameof(AdminPanel),
                        new { warning = "Ten użytkownik posiada już w tę role" });
            }
            if (flag == true)
            {
                var result = _context.UserRoles.FirstOrDefault(x => x.UserId == userRole.User.Id);
                if (result != null)
                {
                    _context.UserRoles.Remove(result);
                    _context.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = userRole.User.Id,
                        RoleId = userRole.Role.Id.ToString()
                    });

                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(AdminPanel));
        }

    }
}