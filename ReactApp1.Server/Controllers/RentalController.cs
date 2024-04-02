using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //A [controller] kiveszi a class nevéből a "controller"-t, az [action] helyére pedig az adott függvény fog bekerülni
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService) 
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> ListRentals()
        {
            var rentalModels = await _rentalService.ListRentals();

            return Ok(rentalModels);
        }

        [HttpGet("{carId}")]  //api/rental/GetRentals/"id"
        public async Task<IActionResult> GetRentals (string carId)
        {
            var rentalsByID = await _rentalService.GetRentals(carId);
            return Ok(rentalsByID);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRentals(string userId)
        {
            var rentalsByUserID = await _rentalService.GetUserRentals(userId);
            return Ok(rentalsByUserID);
        }

        [HttpGet]
        public async Task<IActionResult> ValidDate(string carId, string _fromDate, string _toDate)
        {
            var succeded = await _rentalService.ValidDate(carId, _fromDate, _toDate);
                return Ok(succeded); 
            //return Unauthorized();
        }
        [HttpPost]
        public async Task<IActionResult> NewReservation(string UserId, string CarId, DateTime FromDate, DateTime ToDate, DateTime Created)
        {
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> CountPrice(string carId, string _fromDate, string _toDate)
        {
            var price = await _rentalService.CountPrice(carId, _fromDate, _toDate);
            return Ok(price);
        }
    }
}
