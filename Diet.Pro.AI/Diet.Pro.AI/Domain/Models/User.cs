using Google.Cloud.Firestore;

namespace Diet.Pro.AI.Domain.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string UserId { get; set; } = string.Empty;

        [FirestoreProperty]
        public UserData? UserData { get; set; }

        [FirestoreProperty]
        public UserPhysicalData? UserPhysicalData { get; set; }

    }
}
