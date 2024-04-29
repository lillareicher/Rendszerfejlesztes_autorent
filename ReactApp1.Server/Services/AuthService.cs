using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Models.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AltinnCore.Authentication.JwtCookie;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;


namespace ReactApp1.Server.Services
{
    public interface IAuthService // interface authentication service
    {
        Task<string> Login(Login model);
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

        public async Task<string> Login(Login model)
        {
            Console.WriteLine($"Received username: {model.Username}");
            Console.WriteLine($"Received password: {model.Password}");
            var Hasher = new PasswordHasher<User>();
            var user = await _context.User.AsQueryable().Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == model.Username && u.Password == model.Password);

            if(user == null) 
            {
                throw new AuthenticationException("Unauthorized access");
            }

            List<Claim> claims = new List<Claim>()
            {
                new(ClaimTypes.Role, user.Role.Name),
                new(ClaimTypes.Name, user.UserName)
            };

            var token = GenerateJwtToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateJwtToken(List<Claim> claims)
        {
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jsdgfksdo345634lesdfgdf5jkdgfljkdgfk756lksdf")); // frontend: authorization header -> Bearer token
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken("https://localhost:7045/", "https://localhost:5173/", claims, expires: expires, signingCredentials: signinCredentials);
            return token; 
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

