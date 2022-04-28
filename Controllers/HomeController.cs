using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Models;
using Pdi_Car_Rent.Services;
using System.Diagnostics;

namespace Pdi_Car_Rent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Pdi_Car_RentUserContext _userContext;
        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            Pdi_Car_RentUserContext userContext)
        {
            _logger = logger;
            _userManager = userManager;
            _userContext = userContext;     

            //UserRoleService userRoleService = new UserRoleService(_userContext);
            //userRoleService.StartingSetup(_userContext);

            //ApplicationDbInitializer applicationDbInitializer = new ApplicationDbInitializer(_userManager);
            //applicationDbInitializer.SeedUsers(_userManager);

        }

        public IActionResult Index()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}