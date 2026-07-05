using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToyoCarsClients.Application.Interfaces;
using ToyoCarsClients.Domain.Entities;

namespace ToyoCarsClients.Infraestructure.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        private readonly string secretKey = config.GetSection("Jwt").GetValue<string>("key")!;

        public string GenerateToken(User user)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email.Trim()));
            claims.AddClaim(new Claim("Nombre", user.Name.Trim()));
            claims.AddClaim(new Claim("UserId", user.Id.ToString().Trim()));
            claims.AddClaim(new Claim("TipoDni", user.TipoDNI.ToString().Trim()));
            claims.AddClaim(new Claim("DniNumeber", user.DNINumber.ToString().Trim()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMonths(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);

            return tokencreado;
        }
    }
}
