using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Interfraces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    // Manager Token
    public class TokenService : ITokenService
    {
        // iconfig is data in appsettings.json
        private readonly IConfiguration _config;
        // create key for token
        private readonly SymmetricSecurityKey  _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            // set private key for token
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            
        }
        // create token to user
        public string CreateToken(AppUser user)
        {
            // create claim for token
            var claims = new List<Claim>
            {
                // token will have email and username
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };
            // token use hmacsha512
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            // set token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };
            // create token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}