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
    }
}
