using ReactApp1.Server.DataContext.Model;

namespace ReactApp1.Server.Services
{
    public interface IAuthService // interface authentication service
    {
        Task<bool> Login(LoginModel model);
        Task<List<UserModel>> ListUsers();
        Task<UserModel> GetUser(string username);
    }

    public class AuthService: IAuthService
    {

        public AuthService()
        {

        }
        public async Task<List<UserModel>> ListUsers()
        {
            List <UserModel> users = new List<UserModel>();
            UserModel user1 = new UserModel("u1", "James", "James", "1234");
            UserModel user2 = new UserModel("u2", "John", "John", "123" );
            UserModel user3 = new UserModel("u3", "David", "David", "12345");
            UserModel user4 = new UserModel("u4", "Elisabeth", "Elisabeth", "123456");
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);
            return users;
        }
        public async Task<bool> Login(LoginModel model)
        {

            Console.WriteLine($"Received username: {model.Username}");
            Console.WriteLine($"Received password: {model.Password}");
            var users = await ListUsers();
            foreach( var user in users )
            {
                if(user.UserName==model.Username &&  user.Password==model.Password) return true;
            }
            return false;
        }

        public async Task<UserModel> GetUser(string username)
        {
            var users = await ListUsers();
            foreach( var user in users)
            {
                if(user.UserName==username) return user;
            }
            return new UserModel("0", "0", "0", "0");
        }
   
    }


}

