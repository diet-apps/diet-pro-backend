using Diet.Pro.AI.Aplication.Common.Dtos;
using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Diet.Pro.AI.Infrastructure.Auth
{
    public class AuthService(IConfiguration config) : IAuthService
    {
        private readonly IConfiguration _config = config;

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public AuthTokenResult GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var encripter = _config["Jwt:Secret"]!;
            var key = Encoding.ASCII.GetBytes(encripter);
            var expiresAtUtc = DateTime.UtcNow.AddHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserData?.Name ?? string.Empty)
                }),
                Expires = expiresAtUtc,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthTokenResult
            {
                AccessToken = tokenHandler.WriteToken(token),
                TokenType = "Bearer",
                ExpiresAtUtc = expiresAtUtc
            };
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
