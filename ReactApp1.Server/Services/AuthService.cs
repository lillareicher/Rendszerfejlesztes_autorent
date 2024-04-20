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


namespace ReactApp1.Server.Services
{
    public interface IAuthService // interface authentication service
    {
        Task<JwtPacket> Login(Login model);
        Task<List<User>> ListUsers();
        Task<User> GetUser(string username);
        Task<int> GetUserId(string username);
        Task<string> GetUsername(string username);
        Task<JwtPacket> CreateJwtPacket(User user);
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

        public async Task<JwtPacket> Login(Login model)
        {
            var Hasher = new PasswordHasher<User>();
            var user = _context.User.SingleOrDefault(u => u.UserName == model.Username);
            return CreateJwtPacket(user).Result;

            //Console.WriteLine($"Received username: {model.Username}");
            //Console.WriteLine($"Received password: {model.Password}");
            //var users = await ListUsers();
            //foreach (var user in users)
            //{
            //    if (user.UserName == model.Username && user.Password == model.Password)
            //        return true;
            //}
            //return false;
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

        public async Task<JwtPacket> CreateJwtPacket(User user)
        {

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_KEY_THAT_IS_SECRET"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(user.Id))
            };

            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signinCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtPacket { Token = encodedJwt, Name = user.UserName };
        }


    }


}

