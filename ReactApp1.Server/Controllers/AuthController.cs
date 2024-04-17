using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models.Model;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //A [controller] kiveszi a class nevébõl a "controller"-t, az [action] helyére pedig az adott függvény fog bekerülni
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var succeeded = await _authService.Login(model);

            if (succeeded)
                return Ok();

            return Unauthorized();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser([FromBody] string username)
        {
            var user = await _authService.GetUser(username);
            return Ok(user);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserId([FromBody] string username)
        {
            int id = await _authService.GetUserId(username);
            return Ok(id);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUsername([FromBody] string username)
        {
            int name = await _authService.GetUserId(username);
            return Ok(name);
        }
    }
}
