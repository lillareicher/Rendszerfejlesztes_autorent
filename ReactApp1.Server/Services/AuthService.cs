using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Models.Model;

namespace ReactApp1.Server.Services
{
    public interface IAuthService // interface authentication service
    {
        Task<bool> Login(Login model);
        Task<List<User>> ListUsers();
        Task<User> GetUser(string username);
    }

    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> ListUsers()
        {
            var users = await _context.User.ToListAsync();
            //User user1 = new User("u1", "James", "James", "1234");
            //User user2 = new User("u2", "John", "John", "123" );
            //User user3 = new User("u3", "David", "David", "12345");
            //User user4 = new User("u4", "Elisabeth", "Elisabeth", "123456");
            //users.Add(user1);
            //users.Add(user2);
            //users.Add(user3);
            //users.Add(user4);
            return users;
        }

        public async Task<bool> Login(Login model)
        {

            Console.WriteLine($"Received username: {model.Username}");
            Console.WriteLine($"Received password: {model.Password}");
            var users = await ListUsers();
            foreach (var user in users)
            {
                if (user.UserName == model.Username && user.Password == model.Password)
                    return true;
            }

            return false;
        }

        public async Task<User> GetUser(string username)
        {
            var users = await ListUsers();
            foreach (var user in users)
            {
                if (user.UserName == username)
                    return user;
            }

            return new User();
        }

    }


}

