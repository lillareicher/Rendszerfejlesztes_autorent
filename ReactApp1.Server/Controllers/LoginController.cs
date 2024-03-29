using Microsoft.AspNetCore.Mvc;

namespace elsoBeadandoProba.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {


        [HttpPost(Name = "PostLogin")]
        public IActionResult Login(LoginModel model)
        {
            // Hardcoded username and password for testing
            const string username = "John";
            const string password = "123";

            Console.WriteLine($"Received username: {model.Username}");
            Console.WriteLine($"Received password: {model.Password}");

            if (model.Username == username && model.Password == password)
            {

                Console.WriteLine("Belepes sikeres.");
                return Ok(new { message = "Login successful" });
            }
            else
            {
                Console.WriteLine("Sikertelen belepes.");
                return Unauthorized(new { message = "Invalid username or password in backend" });
            }
        }
    }
}
