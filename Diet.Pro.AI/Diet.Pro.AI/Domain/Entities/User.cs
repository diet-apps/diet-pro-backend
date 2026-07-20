using Google.Cloud.Firestore;

namespace Diet.Pro.AI.Domain.Entities
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string UserId { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Email { get; set; } = string.Empty;

        [FirestoreProperty]
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        [FirestoreProperty]
        public string PasswordHash { get; set; } = string.Empty;

        [FirestoreProperty]
        public UserData? UserData { get; set; }

        [FirestoreProperty]
        public UserPhysicalData? UserPhysicalData { get; set; }

    }
}
