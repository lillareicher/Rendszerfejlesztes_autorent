using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;

namespace ReactApp1.Server.Services
{
    public interface ICarService // interface 
    {
        Task<List<Car>> ListCars();
        Task<List<Car>> FilterCars(string categoryName);
    }

    public class CarService : ICarService
    {
        private readonly ICategoryService _categoryService;
        private readonly DataContext _context;

        public CarService(ICategoryService categoryService, DataContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        public async Task<List<Car>> ListCars()
        {
            var cars = await _context.Car.ToListAsync();
            //Car car1 = new Car() { Id = "c1", CategoryId = "ct1", Brand = "Toyota", Model = "Carmy", DailyPrice = 50 };
            //Car car2 = new Car() { Id = "c2", CategoryId = "ct2", Brand = "Honda", Model = "Civic", DailyPrice = 45 };
            //Car car3 = new Car() { Id = "c3", CategoryId = "ct1", Brand = "Toyota", Model = "Yaris", DailyPrice = 50 };
            //Car car4 = new Car() { Id = "c4", CategoryId = "ct4", Brand = "Ford", Model = "Fiesta", DailyPrice = 50 };

            //cars.Add(car1);
            //cars.Add(car2);
            //cars.Add(car3);
            //cars.Add(car4);

            return cars;
        }

        public async Task<List<Car>> FilterCars(string categoryName)
        {
            var cars = await ListCars();
            var filteredCategories = await _categoryService.FilterCategories(categoryName);
            var filteredCars = new List<Car>();

            foreach (var car in cars)
            {
                foreach (var category in filteredCategories)
                {
                    if (car.CategoryId == category.Id)
                    {
                        filteredCars.Add(car);
                    }
                }
            }

            return filteredCars;
        }
    }


}

