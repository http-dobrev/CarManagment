using CarManagment.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagment.Logic.Interfaces
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car? GetById(int id);
        void Create(Car car);
        void Update(Car car);
        void Delete(int id);
    }
}