using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] 
    public class SalesController: ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<IActionResult> ListSales()
        {
            var sales = await _salesService.ListSales();
            return Ok(sales);
        }

        [HttpGet]
        public async Task<IActionResult> GetSales(int carId)
        {
            var sales = await _salesService.GetSales(carId);
            return Ok(sales);
        }
    }
}
