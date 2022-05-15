﻿using Microsoft.AspNetCore.Identity;
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

            var workerId = _userContext.Roles.First(x => x.Name == "Pracownik").Id;
            var Employees = _userContext.UserRoles.Where(x => x.RoleId == workerId).ToList();
            ViewBag.Employees = _userContext.Users.Where(x => Employees.All(y => y.UserId == x.Id)).ToList();
            ViewBag.RentPlace = _dbContext.CarRentPlace.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AdminPanelRentPlace(MainAdminModel role)
        {
            if (role.roleAction == true)
            {
                var result = _userContext.UserRoles.FirstOrDefault(x => x.UserId == role.RoleModel.User.Id);
                if (result != null)
                {
                    _userContext.UserRoles.Remove(result);
                    _userContext.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = role.RoleModel.User.Id,
                        RoleId = role.RoleModel.Role.Id.ToString()
                    });

                    _userContext.SaveChanges();
                }
            }
            if (role.rentAction == true)
            {
                var result = _dbContext.CarRentPlace.FirstOrDefault(x => x.Id == role.RentPlaceModel.CarRentPlace.Id);
                if (result != null)
                {
                    _dbContext.UserRoles.Remove(result);
                    _userContext.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = role.RoleModel.User.Id,
                        RoleId = role.RoleModel.Role.Id.ToString()
                    });

                    _userContext.SaveChanges();
                }
            }
            return RedirectToAction(nameof(AdminPanel));
        }
    }
}
