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

            var entity = CarViewModelMapper.ToEntity(viewModel);

            _carRepository.Create(entity);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Cars/Edit/5
        public IActionResult Edit(int id)
        {
            var car = _carRepository.GetById(id);
            if (car == null) return NotFound();

            var dto = CarViewModelMapper.ToViewModel(car);
            return View(dto);
        }

        // POST: /Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarViewModel dto)
        {
            if (!ModelState.IsValid)
                return View(dto);


            var entity = CarViewModelMapper.ToEntity(dto);
            _carRepository.Update(entity);

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _carRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
