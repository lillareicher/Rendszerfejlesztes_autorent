using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using ReactApp1.Server.Services;

namespace elsoBeadandoProba.Server.Controllers
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
        public async Task <IActionResult> Login(LoginModel model)
        {
           var successed = await _authService.Login(model);

            if (successed)
                return Ok("Sikeres volt haha hehe xd :)");

            return Unauthorized("Nem jo nev: " +model.Username+ " es nem jo a jelszo se :@: " +model.Password);
        }
    }
}
