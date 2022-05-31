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
using Microsoft.AspNetCore.Identity;
using Pdi_Car_Rent.Areas.Identity.Data;

namespace Pdi_Car_Rent.Data
{
    public class CarsController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepositoryService<CarType> _carTypeRepository;
        private readonly IRepositoryService<CarRentPlaceViewModel> _carPlaceRepository;
        private readonly IRepositoryService<Car> _carRepository;
        private readonly IMapper _mapper;



        public CarsController(
            DatabaseContext databaseContext,
            IRepositoryService<CarType> carTypeRepository,
            IRepositoryService<Car> carRepository,
            IRepositoryService<CarRentPlaceViewModel> carPlaceRepository,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _databaseContext = databaseContext;
            _userManager = userManager;
            _carTypeRepository = carTypeRepository;
            _carPlaceRepository = carPlaceRepository;
            _carRepository = carRepository;
            _mapper = mapper;

        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {

            List<CarIndexViewModel> cars = new();
            if (User.IsInRole("Pracownik"))
            {
                string id = _userManager.GetUserId(User);
                List<Car> Cars = _carRepository
                    .GetAllRecords()
                    .Where(x => x.CarRentPlaceID == _carPlaceRepository
                                                    .GetAllRecords()
                                                    .First(x => x.WorkerId == id).Id).ToList();
                foreach (Car item in Cars)
                {
                    item.CarType = _carTypeRepository.FindBy(x => x.Id == item.CarTypeId).First();
                    item.RentStatus = _databaseContext.RentStatuses.First(x => x.RentStatusId == item.RentStatusID);
                    cars.Add(this._mapper.Map<CarIndexViewModel>(item));
                }

            }
            else if (User.IsInRole("Administrator"))
            {
                cars = _carRepository.GetAllRecords().Select(r => _mapper.Map<CarIndexViewModel>(r)).ToList();
            }
            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carRepository.FindBy(x => x.Id == id);
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
            ViewData["CarRentPlaceID"] = new SelectList(_carPlaceRepository.GetAllRecords(), "Id", "PlaceName");
            ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAllRecords(), "Id", "Name"/*, car.CarTypeId*/);
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Name,RentPriceForHour,CarInfo,CarTypeId,CarRentPlaceID")] Car car)
        {
            //if (ModelState.IsValid)
            //{
            if (User.IsInRole("Pracownik"))
            {
                string id = _userManager.GetUserId(User);
                car.CarRentPlaceID = _carPlaceRepository
                                                .GetAllRecords()
                                                .First(x => x.WorkerId == id).Id;
            }
            _carRepository.Add(car);
            _carRepository.Save();
            return RedirectToAction(nameof(Index));
            //}
            ViewData["CarRentPlaceID"] = new SelectList(_carPlaceRepository.GetAllRecords(), "Id", "PlaceName"/*,car.CarRentPlaceID*/);
            ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAllRecords(), "Id", "Name"/*, car.CarTypeId*/);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        // [HttpGet("Edit/{Id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = _carRepository.GetSingle(id.Value);
            CarEditViewModel model = new CarEditViewModel
            {
                CarId = car.Id,
                CarTypeId = car.CarTypeId,
                RentPriceForHour = car.RentPriceForHour,
                Name = car.Name,
                CarInfo = car.CarInfo,
                CarRentPlaceID = car.CarRentPlaceID,
                //CarTypeList = _context.CarTypes.ToList()
            };

            if (model == null)
            {
                return NotFound();
            }
            ViewData["CarRentPlaceID"] = new SelectList(_carPlaceRepository.GetAllRecords(), "Id", "PlaceName" /*,model.CarRentPlaceID*/);
            ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAllRecords(), "Id", "Name" /*,model.CarTypeId*/);
            return View(model);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarEditViewModel carViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var toEdit = _carRepository.GetSingle(carViewModel.CarId);

                    _mapper.Map<CarEditViewModel, Car>(carViewModel, toEdit);

                    _carRepository.Edit(toEdit);

                    _carRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CarExists(car.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CarRentPlaceID"] = new SelectList(_carPlaceRepository.GetAllRecords(), "Id", "PlaceName", car.CarRentPlaceID);
            //ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAllRecords(), "Id", "Name", car.CarTypeId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carRepository.FindBy(m => m.Id == id);
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
            var car = _carRepository.FindBy(x => x.Id == id).First();
            _carRepository.Delete(car);
            _carRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _carRepository.FindBy(e => e.Id == id).Any();
        }
    }
}
