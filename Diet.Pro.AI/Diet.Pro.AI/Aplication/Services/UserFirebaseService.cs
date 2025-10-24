using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Domain.Models;
using Diet.Pro.AI.Infra.Data.Repositories.Collections;
using Google.Cloud.Firestore;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Services
{
    public class UserFirebaseService : IUserFirebaseService
    {
        private readonly FirestoreDb _firestoreDb;

        public UserFirebaseService(FirestoreDb firestoreDb)
            => _firestoreDb = firestoreDb;

        public async Task<Result<User>> CreateUserDataAsync(User user)
        {
            var docRef = _firestoreDb.Collection(FirebaseCollections.Users).Document(user.UserId);
            await docRef.SetAsync(user);
            return user;
        }

        public async Task<Result<User>> CreateUserPhysicalDataAsync(User user)
        {
            var docRef = _firestoreDb.Collection(FirebaseCollections.Users).Document(user.UserId);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return new Exception("Usuário não encontrado");

            var _user = snapshot.ConvertTo<User>();
            _user.UserPhysicalData = user.UserPhysicalData;

            await docRef.SetAsync(_user, SetOptions.Overwrite);

            return _user;
        }

        public async Task<Result<User>> GetUserByIdAsync(string userId)
        {
            var docRef = _firestoreDb.Collection(FirebaseCollections.Users).Document(userId);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return new Exception("Usuário não encontrado");

            var user = snapshot.ConvertTo<User>();
            return user;
        }
        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            var usersRef = _firestoreDb.Collection(FirebaseCollections.Users);
            var query = usersRef.WhereEqualTo("Email", email);
            var snapshot = await query.GetSnapshotAsync();

            if (snapshot.Documents.Count == 0)
                return new Exception("Usuário não encontrado");

            var doc = snapshot.Documents.First();
            var user = doc.ConvertTo<User>();
            return user;
        }

    }
}
