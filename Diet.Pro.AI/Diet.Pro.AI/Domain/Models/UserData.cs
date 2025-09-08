using Google.Cloud.Firestore;

namespace Diet.Pro.AI.Domain.Models
{
    [FirestoreData]
    public class UserData
    {
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;

        [FirestoreProperty]
        public DateTime DateOfBirth { get; set; }

        [FirestoreProperty]
        public string Sex { get; set; } = string.Empty;
    }
}
