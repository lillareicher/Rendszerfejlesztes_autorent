using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Models.Model;

namespace ReactApp1.Server.Services
{
    public interface ICarService // interface 
    {
        Task<List<Car>> ListCars();
        Task<List<Car>> FilterCars(string categoryName);
        Task<bool> AddCars(NewCar newC);
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

        public async Task<bool> AddCars(NewCar newC)
        {
            var cars = await ListCars();
            Car car = new Car();

            car.CategoryId = newC.CategoryId;
            car.Brand = newC.Brand;
            car.Model = newC.Model;
            car.DailyPrice = newC.DailyPrice;

            var categoryIdExists = await _context.Category.FirstOrDefaultAsync(c => c.Id == newC.CategoryId);

            if(categoryIdExists == null)
            {
                return false;
            }

            await _context.AddAsync(car);
            await _context.SaveChangesAsync();
            return true;
        }

        }
    }




