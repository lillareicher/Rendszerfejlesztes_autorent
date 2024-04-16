using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetRentals(int carId)
        {
            var rentalsByID = await _rentalService.GetRentals(carId);
            return Ok(rentalsByID);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRentals(string Username)
        {
            var rentalsByUserID = await _rentalService.GetUserRentals(Username);
            return Ok(rentalsByUserID);
        }

        [HttpGet]
        public async Task<IActionResult> ValidDate(int carId, string _fromDate, string _toDate)
        {
            var succeded = await _rentalService.ValidDate(carId, _fromDate, _toDate);
            return Ok(succeded);
            //return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> CountPrice(int carId, string _fromDate, string _toDate)
        {
            var price = await _rentalService.CountPrice(carId, _fromDate, _toDate);
            return Ok(price);
        }

        [HttpGet]
        public async Task<IActionResult> NewReservation(int userId, int carId, string _fromDate, string _toDate)
        {
            var succeded = await _rentalService.NewReservation(userId, carId, _fromDate, _toDate);

            return Ok(succeded);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReservation(int rentalId)
        {
            var succeded = await _rentalService.DeleteReservation(rentalId);
            return Ok(succeded);
        }
    }
}
