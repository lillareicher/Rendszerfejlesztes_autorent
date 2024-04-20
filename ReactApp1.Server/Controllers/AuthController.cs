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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;

        public AuthController(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var succeeded = await _authService.Login(model);
            var user = _context.User.SingleOrDefault(u => u.UserName == model.Username);
            if (succeeded && user != null)
            {
                return Ok(CreateJwtPacket(user));
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

        JwtPacket CreateJwtPacket(User user)
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