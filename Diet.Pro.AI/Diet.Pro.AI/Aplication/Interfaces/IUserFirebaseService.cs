using Diet.Pro.AI.Domain.Models;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Interfaces
{
    public interface IUserFirebaseService
    {
        Task<Result<User>> CreateUserDataAsync(User user);
        Task<Result<User>> CreateUserPhysicalDataAsync(User user);
        Task<Result<User>> GetUserByIdAsync(string userId);      
        Task<Result<User>> GetUserByEmailAndPassword(string email, string password);
    }
}
