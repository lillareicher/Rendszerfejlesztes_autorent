using ReactApp1.Server.DataContext.Model;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
namespace ReactApp1.Server.Services
{
    public interface ICarService // interface 
    {
        Task<List<CarModel>> ListCars();
        Task<List<CarModel>> FilterCars(string categoryName);
    }

    public class CarService : ICarService
    {
        private readonly ICategoryService _categoryService;

        public CarService(ICategoryService categoryService) 
        { 
            _categoryService = categoryService;
        }

        public async Task<List<CarModel>> ListCars()
        {
            List<CarModel> cars = new List<CarModel>();
            CarModel car1 = new CarModel() { Id = "c1", CategoryId = "1", Brand = "Toyota", Model = "Carmy", DailyPrice = "50$" };
            CarModel car2 = new CarModel() { Id = "c2", CategoryId = "2", Brand = "Honda", Model = "Civic", DailyPrice = "45$" };
            CarModel car3 = new CarModel() { Id = "c3", CategoryId = "1", Brand = "Toyota", Model = "Yaris", DailyPrice = "50$" };
            CarModel car4 = new CarModel() { Id = "c4", CategoryId = "4", Brand = "Ford", Model = "Fiesta", DailyPrice = "50$" };

            cars.Add(car1);
            cars.Add(car2);
            cars.Add(car3);
            cars.Add(car4);

            return cars;
        }

        public async Task<List<CarModel>> FilterCars(string categoryName)
        {
            var cars = await ListCars();
            var filteredCategories = await _categoryService.FilterCategories(categoryName);
            var filteredCars = new List<CarModel>();

            foreach (var car in cars) 
            { 
                foreach(var category in filteredCategories)
                {
                    if(car.CategoryId == category.Id)
                    {
                        filteredCars.Add(car);
                    }
                }
            }

            return filteredCars;
        }
    }


}

