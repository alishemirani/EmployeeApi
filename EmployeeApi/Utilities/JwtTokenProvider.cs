using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EmployeeApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeApi.Utilities
{
    public static class JwtTokenProvider
    {
        public static string GenerateToken(TokenConfig config, User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Name, user.FullName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            
            claims.AddRange(user.Roles.Select(t=> new Claim(ClaimTypes.Role, t)));

            var token = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}