using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Models;
using Pdi_Car_Rent.Services;

namespace Pdi_Car_Rent.Controllers
{
    public class CarRentalsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public CarRentPlaceItemViewModel Model { get; set; }
        public CarRentalsController(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            //if (_context.CarRentPlace.Count() < 1 && _context.ArchivedCarRentalPlaces.Count() < 1)
            //{
            //    _context.CarRentPlace.Add(
            //        new CarRentPlaceViewModel()
            //        {
            //            Address = "adres1",
            //            PlaceName = "Wypożyczalnia1",
            //            WorkerId = Guid.NewGuid().ToString(),
            //        });
            //    _context.SaveChanges();
            //}
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
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_context.CarRentPlace.FirstOrDefault(x => x.Id == id));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            return View(_context.CarRentPlace.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public IActionResult Edit(CarRentPlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                ICarRentPlace toEdit = _context.CarRentPlace.First(x => x.Id == model.Id);
                ToArchive(toEdit);
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public void ToArchive(ICarRentPlace toArchive)
        {
            _context.ArchivedCarRentalPlaces.Add(_mapper.Map<ArchivedCarRentalPlaces>(toArchive));
            _context.Remove(toArchive);
            _context.SaveChanges();
        }
        [HttpGet]
        public IActionResult Archive(int id)
        {
            ICarRentPlace toArchive = _context.CarRentPlace.First(x => x.Id == id);
            ToArchive(toArchive);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Archived()
        {
            return View(_context.ArchivedCarRentalPlaces.ToList());
        }

    }
}
