using Diet.Pro.AI.Aplication.Common.Dtos;
using Diet.Pro.AI.Domain.Entities;

namespace Diet.Pro.AI.Aplication.Interfaces
{
    public interface IAuthService
    {
        bool VerifyPassword(string password, string hash);
        AuthTokenResult GenerateJwtToken(User user);
        string HashPassword(string password);
    }
}
