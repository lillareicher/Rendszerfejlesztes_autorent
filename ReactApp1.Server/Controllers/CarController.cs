using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Migrations;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Services;
using System.Data;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //A [controller] kiveszi a class nevéből a "controller"-t, az [action] helyére pedig az adott függvény fog bekerülni
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        //[Authorize(Roles = $"Admin, User")]
        [HttpGet]
        public async Task<IActionResult> ListCars()
        {
            List<Car> cars = await _carService.ListCars();

            return Ok(cars);
        }

        //[Authorize(Roles = $"Admin, User")]
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> FilterCars(string categoryName)
        {
            var categories = await _carService.FilterCars(categoryName);
            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> AddCar(int categoryId, string brand, string model, int dailyPrice)
        {
            var cars = await _carService.AddCars(categoryId, brand, model, dailyPrice);
            return Ok(cars);
        }
    }
}