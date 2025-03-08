using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DOCOSoft.UserAPI.Services
{
    public class JwtService(IConfiguration config)
    {
        public string GenerateToken(string? userId, string email, string role)
        {
            var key = Encoding.ASCII.GetBytes(config["JwtSettings:SecretKey"]!);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId!),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                config["JwtSettings:Issuer"],
                config["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
