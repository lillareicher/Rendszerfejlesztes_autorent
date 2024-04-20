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
    public interface ITokenService
    {
        Task<JwtPacket> CreateJwtPacket(User user);
    }
    public class TokenService: ITokenService
    {
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
