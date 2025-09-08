namespace Diet.Pro.AI.Domain.Models
{
    public class UserPhysicalData
    {
        public int Height { get; set; }
        public double Weight { get; set; }
        public string PhysicalActivityLevel { get; set; } = string.Empty;
        public double? FatPercentage { get; set; } 
    }
}
