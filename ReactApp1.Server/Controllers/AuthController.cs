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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var login = await _authService.Login(model);
            return Ok(login);      
        }

        [Authorize(Roles = $"Admin, User")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(/*[FromBody]*/ string username)
        {
            var user = await _authService.GetUser(username);
            return Ok(user);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserId(/*[FromBody]*/ string username)
        {
            int id = await _authService.GetUserId(username);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUsername(/*[FromBody]*/ string username)
        {
            int name = await _authService.GetUserId(username);
            return Ok(name);
        }

        
    }
}