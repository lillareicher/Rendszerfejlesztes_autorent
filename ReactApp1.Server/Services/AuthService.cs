using ReactApp1.Server.DataContext.Model;

namespace ReactApp1.Server.Services
{
    public interface IAuthService // interface authentication service
    {
        Task<bool> Login(LoginModel model);
    }

    public class AuthService: IAuthService
    {

        public AuthService()
        {

        }

        public async Task<bool> Login(LoginModel model)
        {
            const string username = "John";
            const string password = "123";

            Console.WriteLine($"Received username: {model.Username}");
            Console.WriteLine($"Received password: {model.Password}");

            if (model.Username == username && model.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
   
    }


}

