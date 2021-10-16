using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace UStart.Domain.Helpers.TokenHelper
{
    public class TokenHelper
    {
        private readonly TokenContext tokenContext;
        public TokenHelper(TokenContext tokenContext)
        {
            this.tokenContext = tokenContext;
        }

        public string GenerateToken(string identifier, string name, string email, string role)
        {
            var clains = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.NameIdentifier, identifier),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)

                };

            return GenerateToken(clains);            
        }

        public string GenerateToken(params Claim[] clains)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = tokenContext.getTokenBytes();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(clains),
                Expires = DateTime.UtcNow.AddMinutes(tokenContext.ExpiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}