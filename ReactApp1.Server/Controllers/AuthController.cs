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
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //A [controller] kiveszi a class nevébõl a "controller"-t, az [action] helyére pedig az adott függvény fog bekerülni
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;

        public AuthController(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var Hasher = new PasswordHasher<User>();
            var user = _context.User.SingleOrDefault(u => u.UserName == model.Username);
            if (user != null)
            {
                if (0 != Hasher.VerifyHashedPassword(user, user.Password, model.Password))
                {
                    return Ok(_authService.CreateJwtPacket(user));
                }
                return NotFound("Email and/or Password are incorrect");
            }
            return NotFound("Email and/or Password are incorrect");
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