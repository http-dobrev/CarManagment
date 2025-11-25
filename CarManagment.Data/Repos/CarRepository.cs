using System.Data;
using CarManagment.Data.DTOs;
using CarManagment.Data.Mappers;
using CarManagment.Logic.Entities;
using CarManagment.Logic.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace CarManagment.Data.Repos
{
    public class CarRepository : ICarRepository
    {
        private readonly string _connectionString;

        public CarRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }

        // CREATE Car
        public void Create(Car car)
        {
            CarDto dto = CarDataMapper.ToDto(car);

            const string sql = @"INSERT INTO dbo.Cars (Brand, Model, Year, Price)
                                VALUES (@Brand, @Model, @Year, @Price)";

            var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Brand", dto.Brand);
            cmd.Parameters.AddWithValue("@Model", dto.Model);
            cmd.Parameters.AddWithValue("@Year", dto.Year);
            cmd.Parameters.AddWithValue("@Price", dto.Price);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // READ All Cars
        public IEnumerable<Car> GetAll()
        {
            var cars = new List<Car>();

            const string sql = @"SELECT Id, Brand, Model, Year, Price FROM dbo.Cars ORDER BY Id DESC";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            conn.Open();
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var dto = new CarDto
                {
                    Id = rdr.GetInt32(0),
                    Brand = rdr.GetString(1),
                    Model = rdr.GetString(2),
                    Year = rdr.GetInt32(3),
                    Price = rdr.IsDBNull(4) ? null : rdr.GetDecimal(4)
                };

                cars.Add(CarDataMapper.ToEntity(dto));
            }

            rdr.Close();
            conn.Close();
           
            return cars;
        }

        // Read One Car
        public Car? GetById(int id)
        {
            const string sql = @"SELECT Id, Brand, Model, Year, Price FROM dbo.Cars WHERE Id = @Id";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            conn.Open();
            using var rdr = cmd.ExecuteReader();

            if (!rdr.Read()) return null;

            var dto = new CarDto
            {
                Id = rdr.GetInt32(0),
                Brand = rdr.GetString(1),
                Model = rdr.GetString(2),
                Year = rdr.GetInt32(3),
                Price = rdr.IsDBNull(4) ? null : rdr.GetDecimal(4)
            };

            rdr.Close();
            conn.Close();

            return CarDataMapper.ToEntity(dto);
        }

        // UPDATE a Car
        public void Update(Car car)
        {
            CarDto dto = CarDataMapper.ToDto(car);

            const string sql = @"UPDATE dbo.Cars
                                 SET Brand = @Brand, Model = @Model, Year = @Year, Price = @Price
                                 WHERE Id = @Id";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Brand", dto.Brand);
            cmd.Parameters.AddWithValue("@Model", dto.Model);
            cmd.Parameters.AddWithValue("@Year", dto.Year);
            cmd.Parameters.AddWithValue("@Price", (object?)dto.Price ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", dto.Id);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // Delete Car
        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM dbo.Cars WHERE Id = @Id", conn);

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
