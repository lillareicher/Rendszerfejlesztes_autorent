using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Services;

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

        [HttpGet]
        public async Task<IActionResult> ListCars()
        {
            List<Car> cars = await _carService.ListCars();

            return Ok(cars);
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> FilterCars(string categoryName)
        {
            var categories = await _carService.FilterCars(categoryName);
            return Ok(categories);
        }
    }
}