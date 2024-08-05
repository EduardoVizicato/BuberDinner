using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super-secret-key")),
                SecurityAlgorithms.HmacSha256
        );

            var claims = new []{
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),

            };

            var securityToken = new JwtSecurityToken(issuer : "BuberDinner",
            expires : DateTime.Now.AddDays(1),
            claims : claims, 
            signingCredentials : signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}