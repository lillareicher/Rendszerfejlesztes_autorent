using ReactApp1.Server.DataContext.Model;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
namespace ReactApp1.Server.Services
{
    public interface ICarService // interface 
    {
        Task<List<CarModel>> ListCars();
    }

        public class CarService: ICarService
    {

        public async Task<List<CarModel>> ListCars()
        {
            List<CarModel> Cars = new List<CarModel>();
            string text = File.ReadAllText(@"./Cars.json");
            var car = JsonSerializer.Deserialize<CarModel>(text);
            Cars.Add(car);
            return Cars;

        }

    }


}

