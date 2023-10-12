using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handlers
{
    public class JWTokenHandler : IJWTokenHandler
    {
        private readonly IConfiguration _configuration;
        public JWTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //membutat method untuk generate token jwt 
        public string Generate(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey , SecurityAlgorithms.HmacSha256);
            var tokenOpts = new JwtSecurityToken(
                issuer: _configuration["JWTService:Issuer"],
                audience: _configuration["JWTService:Audience"],
                expires: DateTime.Now.AddMinutes(6),
                signingCredentials: signingCredentials,
                claims: claims
                );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOpts);
            return encodedToken;
        }
    }
}
