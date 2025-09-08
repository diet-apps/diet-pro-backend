namespace Diet.Pro.AI.Infra.Shared.InputModels
{
    public class UserPhysicalDataInputModel
    {
        public int Height { get; set; }
        public double Weight { get; set; }
        public string PhysicalActivityLevel { get; set; } = string.Empty;
        public double? FatPercentage { get; set; }
    }
}
