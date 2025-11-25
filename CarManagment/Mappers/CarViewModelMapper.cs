using CarManagment.Models;
using CarManagment.Logic.Entities;

namespace CarManagment.Mappers
{
    public static class CarViewModelMapper
    {
        public static CarViewModel ToViewModel(Car entity)
        {
            return new CarViewModel
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Year = entity.Year,
                Price = entity.Price
            };
        }

        public static Car ToEntity(CarViewModel vm)
        {
            return new Car
            {
                Id = vm.Id,
                Brand = vm.Brand,
                Model = vm.Model,
                Year = vm.Year,
                Price = vm.Price
            };
        }
    }
}
