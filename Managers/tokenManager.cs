using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;

namespace Managers
{
    public class tokenManager
    {
        private readonly IConfiguration configuration;
        public tokenManager(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string generateToken(IdentityUser user)
        {
            //to obtain the key and other settings from appsettings.json
            var jwtSettings = configuration.GetSection("Jwtsettings");
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            //to embed key in credentials
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])) ;
            Console.WriteLine("key in tokenmanager: "+jwtSettings["Key"]);

            //to embed credentials in token
            var credentialas = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);
            //to generate the token
            var token = new JwtSecurityToken(
                //issuer: configuration["Jwtsettings:Issuer"],
                //audience: configuration["Jwtsettings:Audience"],
                claims: claims,
                signingCredentials: credentialas,
                expires: DateTime.Now.AddDays(30)
                );
           
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
