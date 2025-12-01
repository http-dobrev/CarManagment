using CarManagment.Logic.Entities;
using CarManagment.Logic.Services;

namespace CarManagment.Logic.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car? GetById(int id);

        ServiceResult Create(Car car);
        ServiceResult Update(Car car);
        ServiceResult Delete(int id);
    }
}