using Google.Cloud.Firestore;

namespace Diet.Pro.AI.Domain.Models
{
    [FirestoreData]
    public class UserPhysicalData
    {
        [FirestoreProperty]
        public int Height { get; set; }

        [FirestoreProperty]
        public double Weight { get; set; }

        [FirestoreProperty]
        public string PhysicalActivityLevel { get; set; } = string.Empty;

        [FirestoreProperty]
        public double? FatPercentage { get; set; } 
    }
}
