namespace Diet.Pro.AI.Domain.Models
{
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public UserData? UserData { get; set; }
        public UserPhysicalData? UserPhysicalData { get; set; }
    }
}
