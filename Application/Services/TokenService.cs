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

namespace Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenService(IConfiguration configuration,UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager; 
        }

        public async Task<string> GenerateToken(IdentityUser user)
        {
            //to obtain the key and other settings from appsettings.json
            var jwtSettings = _configuration.GetSection("Jwtsettings");
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
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
