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
        Task<int> GetUserId(string username);
        Task<string> GetUsername(string username);
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

        public async Task<int> GetUserId (string username)
        {
            var users = await ListUsers();
            foreach (var user in users)
            {
                if (user.UserName == username)
                    return user.Id;
            }
            return 0;
        }

        public async Task<string> GetUsername (string username)
        {
            var users = await ListUsers();
            foreach (var user in users)
            {
                if (user.UserName == username)
                    return user.UserName;
            }
            return "";
        }

    }


}

