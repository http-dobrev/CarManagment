using CarManagment.Logic.Entities;
using CarManagment.Logic.Interfaces;

namespace CarManagment.Logic.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public Car? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _carRepository.GetById(id);
        }

        public ServiceResult Create(Car car)
        {
            if (car == null)
                return ServiceResult.Failure("Car cannot be null.");

            var errors = ValidateCar(car);

            if (errors.Any())
                return ServiceResult.Failure(errors);

            _carRepository.Create(car);
            return ServiceResult.Success();
        }
        public ServiceResult Update(Car car)
        {
            if (car == null)
                return ServiceResult.Failure("Car cannot be null.");

            if (car.Id <= 0)
                return ServiceResult.Failure("Car Id must be greater than 0.");

            var errors = ValidateCar(car);

            if (errors.Any())
                return ServiceResult.Failure(errors);

            var existing = _carRepository.GetById(car.Id);

            if (existing == null)
                return ServiceResult.Failure("Car not found.");


            _carRepository.Update(car);
            return ServiceResult.Success();
        }

        public ServiceResult Delete(int id)
        {
            if (id <= 0)
                return ServiceResult.Failure("Id must be greater than 0.");

            var existing = _carRepository.GetById(id);
            if (existing == null)
                return ServiceResult.Failure("Car not found.");

            _carRepository.Delete(id);
            return ServiceResult.Success();
        }

        public List<string> ValidateCar(Car car)
        {
            var errors = new List<string>();
            errors.Clear();
            if (string.IsNullOrWhiteSpace(car.Brand))
            {
                errors.Add("Brand is required.");
            }
            if (string.IsNullOrWhiteSpace(car.Model))
            {
                errors.Add("Model is required.");
            }
            if (car.Year < 1886 || car.Year > DateTime.UtcNow.Year + 1)
            {
                errors.Add("Year is invalid.");
            }
            if (car.Price.HasValue && car.Price < 0)
            {
                errors.Add("Price cannot be negative.");

            }
            else
            {
                errors.Add("There is no price.");
            }

            return errors;
        }
    }
}
