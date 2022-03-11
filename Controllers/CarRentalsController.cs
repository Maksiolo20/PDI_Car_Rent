using Microsoft.AspNetCore.Mvc;
using PdI_Car_Rent.Models;

namespace PdI_Car_Rent.Controllers
{
    public class CarRentalsController : Controller
    {
        public CarRentPlaceItemViewModel _model { get; set; } = new CarRentPlaceItemViewModel();
        public CarRentalsController()
        {
            
            _model.CarRentPlacesList.Add(new CarRentPlaceViewModel() { PlaceId = 1, Address = "adres1", PlaceName = "Wypożyczalnia1" });
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            return View(_model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarRentPlaceViewModel model)
        {
            _model.CarRentPlacesList.Add(model);
            return RedirectToAction(nameof (Index));
        }
        [HttpGet ("Id")]
        public IActionResult Details(int id)
        {
            return View(_model.CarRentPlacesList.FirstOrDefault(x=>x.PlaceId==id));
        }
    }
}
