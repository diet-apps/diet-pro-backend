using Diet.Pro.AI.Domain.Models;

namespace Diet.Pro.AI.Aplication.Interfaces
{
    public interface IAuthService
    {
        bool VerifyPassword(string password, string hash);
        string GenerateJwtToken(User user);
        string HashPassword(string password);
    }
}
