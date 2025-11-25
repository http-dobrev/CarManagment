using CarManagment.Data.DTOs;
using CarManagment.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagment.Data.Mappers
{
    public static class CarDataMapper
    {
        public static Car ToEntity(CarDto dto)
        {
            return new Car
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Price = dto.Price
            };
        }

        public static CarDto ToDto(Car entity)
        {
            return new CarDto
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Year = entity.Year,
                Price = entity.Price
            };
        } 
    }
}
