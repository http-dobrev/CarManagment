using Microsoft.AspNetCore.Mvc;
using CarManagment.Logic.Interfaces;
using CarManagment.Logic.Services;
using CarManagment.Mappers;
using CarManagment.Models;


namespace CarManagment.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carRepository;

        public CarsController(ICarService CarService)
        {
            _carRepository = CarService;
        }


        // GET: /Cars
        public IActionResult Index()
        {
            var cars = _carRepository.GetAll();

            var viewModels = cars
                .Select(CarViewModelMapper.ToViewModel)
                .ToList();

            return View(viewModels);
        }

        // GET: /Cars/Create
        public IActionResult Create()
        {
            return View(new CarViewModel());
        }

        // POST: /Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var carEntity = CarViewModelMapper.ToEntity(viewModel);

            var result = _carRepository.Create(carEntity);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Cars/Edit/5
        public IActionResult Edit(int id)
        {
            var car = _carRepository.GetById(id);
            if (car == null) return NotFound();

            var viewModel = CarViewModelMapper.ToViewModel(car);
            return View(viewModel);
        }

        // POST: /Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                ModelState.AddModelError(string.Empty, "Id mismatch.");
                return View(viewModel);
            }

            if (!ModelState.IsValid)
                return View(viewModel);

            var carEntity = CarViewModelMapper.ToEntity(viewModel);
            
            var result = _carRepository.Update(carEntity);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: /Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _carRepository.Delete(id);

            if (!result.IsSuccess)
            {
                // Handle deletion errors if necessary
                TempData["Error"] = string.Join(" ", result.Errors);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
