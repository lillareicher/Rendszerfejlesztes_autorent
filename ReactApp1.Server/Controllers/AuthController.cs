using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //A [controller] kiveszi a class nev�b�l a "controller"-t, az [action] hely�re pedig az adott f�ggv�ny fog beker�lni
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task <IActionResult> Login(LoginModel model)
        {
           var succeeded = await _authService.Login(model);

            if (succeeded)
                return Ok();

            return Unauthorized();
        }
    }
}
