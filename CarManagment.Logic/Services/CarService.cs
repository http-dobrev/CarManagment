using CarManagment.Logic.Entities;
using CarManagment.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _carRepository.GetById(id);
        }

        public void Create(Car car)
        {
            _carRepository.Create(car);
        }
        public void Update(Car car)
        {
            _carRepository.Update(car);
        }

        public void Delete(int id)
        {
            _carRepository.Delete(id);
        }

    }
}
