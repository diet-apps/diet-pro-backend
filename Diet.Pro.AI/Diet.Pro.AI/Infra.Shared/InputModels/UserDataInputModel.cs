using Diet.Pro.AI.Domain.Models;

namespace Diet.Pro.AI.Infra.Shared.InputModels
{
    public class UserDataInputModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; } = string.Empty;
    }
}
