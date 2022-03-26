using Microsoft.AspNetCore.Mvc;
using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Models;

namespace Pdi_Car_Rent.Controllers
{
    public class CarRentalsController : Controller
    {
        private readonly DatabaseContext _context;
        public CarRentPlaceItemViewModel Model { get; set; }
        public CarRentalsController(DatabaseContext context)
        {
            _context = context;
            _context.CarRentPlace.Add(
                new CarRentPlaceViewModel() { 
                    Address = "adres1",
                    PlaceName = "Wypożyczalnia1",
                    Cars = new List<Car>(){
                        new Car(){Name = "new car"}
                    }
                });
            _context.SaveChanges();
            Model = new CarRentPlaceItemViewModel(_context);
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            return View(Model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarRentPlaceViewModel model)
        {
            _context.CarRentPlace.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof (Index));
        }
        [HttpGet ("Id")]
        public IActionResult Details(int id)
        {
            return View(_context.CarRentPlace.FirstOrDefault(x=>x.PlaceId==id));
        }
    }
}
