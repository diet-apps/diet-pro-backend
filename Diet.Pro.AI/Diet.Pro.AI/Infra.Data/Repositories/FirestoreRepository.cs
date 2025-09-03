using Diet.Pro.AI.Infra.Data.Repositories.Interfaces;
using Google.Cloud.Firestore;
using OperationResult;

namespace Diet.Pro.AI.Infra.Data.Repositories
{
    public class FirestoreRepository : IFireStoreRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreRepository(FirestoreDb firestoreDb) 
            => _firestoreDb = firestoreDb;

        public async Task<Result<string>> AddDoc<T>(string collectionName, T document) where T : class
        {
            try
            {
                var collection = _firestoreDb.Collection(collectionName);
                var docRef = await collection.AddAsync(document);
                return Result.Success(docRef.Id);
            }
            catch (Exception ex)
            {
                return ex;

            }
        }

        public async Task<Result> DeleteDoc(string collectionName, string documentId)
        {
            try
            {
                var collection = _firestoreDb.Collection(collectionName);
                var docRef = collection.Document(documentId);
                await docRef.DeleteAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public async Task<Result> UpdateDoc<T>(string collectionName, string documentId, T document) where T : class
        {
            try
            {
                var collection = _firestoreDb.Collection(collectionName);
                var docRef = collection.Document(documentId);
                await docRef.SetAsync(document, SetOptions.Overwrite);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public async Task<T?> GetDocById<T>(string collectionName, string documentId) where T : class
        {
            try
            {
                var collection = _firestoreDb.Collection(collectionName);
                var docRef = collection.Document(documentId);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return null;

                return snapshot.ConvertTo<T>();
            }
            catch
            {
                return null;
            }
        }
    }
}
