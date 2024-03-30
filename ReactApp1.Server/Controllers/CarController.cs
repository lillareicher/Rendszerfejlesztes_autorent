using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //A [controller] kiveszi a class nevéből a "controller"-t, az [action] helyére pedig az adott függvény fog bekerülni
    public class CarController : ControllerBase
    {
        //private readonly ICarService _carService;

        //public CarController(ICarService carService)
        //{
        //    _carService = carService;
        //}

        [HttpGet]
        public async Task<IActionResult> ListCars()
        {
            //List<CarModel> cars = await _carService.ListCars();
            List<CarModel> cars = new List<CarModel>();
            CarModel car1=new CarModel();
            CarModel car2=new CarModel();
            car1.Id = "c1";
            car1.CategoryId = "1";
            car1.Brand = "Toyota";
            car1.Model = "Carmy";
            car1.DailyPrice = "50$";

            car2.Id = "c2";
            car2.CategoryId = "2";
            car2.Brand = "Honda";
            car2.Model = "Civic";
            car2.DailyPrice = "45$";

            cars.Add(car1);
            cars.Add(car2);

            return Ok(cars);
        }
    }
}
