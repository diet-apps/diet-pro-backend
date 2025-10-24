using Diet.Pro.AI.Domain.Models;
using Google.Cloud.Firestore;

namespace Diet.Pro.AI.Infra.Shared.InputModels
{
    public class UserDataInputModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
