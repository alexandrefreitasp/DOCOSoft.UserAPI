using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DOCOSoft.UserAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DOCOSoft.UserAPI.Services
{    public class JwtService(IConfiguration configuration) : IJwtService
    {
        private readonly string _secretKey = configuration["JwtSettings:SecretKey"]!;
        private readonly string _issuer = configuration["JwtSettings:Issuer"]!;
        private readonly string _audience = configuration["JwtSettings:Audience"]!;

        public string GenerateToken(string userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    ClockSkew = TimeSpan.Zero
                }, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
   
}
