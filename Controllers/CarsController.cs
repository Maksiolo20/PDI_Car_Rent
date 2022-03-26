#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pdi_Car_Rent.Models;
using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Data
{
    public class CarsController : Controller
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public List<CarType> Types { get; set; } = new() {
            new CarType { /*CarTypeId = 1,*/ Name = "Combi" }, 
            new CarType { /*CarTypeId = 2,*/ Name = "Sedan" } };
        public List<CarRentPlaceViewModel> Places { get; set; } = new() { 
            new CarRentPlaceViewModel { /*CarTypeId = 1,*/ PlaceName = "Bielsko-Biała", Address="Willowa 2" }, 
            new CarRentPlaceViewModel { /*CarTypeId = 2,*/ PlaceName = "Katowice", Address="Shooperów 96" } };
        public CarsController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            foreach (var item in Types)
            {
                if (_context.CarTypes.Select(x => x.Name == item.Name).Any())
                    continue;
                else
                _context.CarTypes.Add(item);
            }

            foreach (var item in Places)
            {
                if (_context.CarRentPlace.Select(x => x.PlaceName == item.PlaceName).Any())
                    continue;
                else
                    _context.CarRentPlace.Add(item);
            }

            _context.SaveChanges();
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cars.Include(s => s.CarType);
            var Cars = applicationDbContext.ToList();
            var viewModel = Cars.Select(r => _mapper.Map<CarIndexViewModel>(r));
            return View(viewModel);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(s => s.CarType)
                .FirstOrDefaultAsync(m => m.CarId == id);
            var viewModel = _mapper.Map<CarDetailsViewModel>(car);
            if (car == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["CarRentPlaceID"] = new SelectList(_context.CarRentPlace, "PlaceId", "PlaceName");
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "CarTypeId", "Name"/*, car.CarTypeId*/);
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Name,RentPriceForHour,CarInfo,CarTypeId,CarRentPlaceID")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarRentPlaceID"] = new SelectList(_context.CarRentPlace, "PlaceId", "PlaceName", car.CarRentPlaceID);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "CarTypeId", "Name"/*, car.CarTypeId*/);
            return View();
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = _context.Cars.Find(id);
            CarEditViewModel model = new CarEditViewModel
            {
                CarTypeList = _context.CarTypes.ToList()
            };

            if (model == null)
            {
                return NotFound();
            }
            ViewData["CarRentPlaceID"] = new SelectList(_context.CarRentPlace, "PlaceId", "PlaceId", model.CarRentPlaceID);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "CarTypeId", "CarTypeId", model.CarTypeId);
            return View(model);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Name,RentPriceForHour,CarInfo,CarTypeId,CarRentPlaceID")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarRentPlaceID"] = new SelectList(_context.CarRentPlace, "PlaceId", "PlaceId", car.CarRentPlaceID);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "CarTypeId", "CarTypeId", car.CarTypeId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarRentPlace)
                .Include(c => c.CarType)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
