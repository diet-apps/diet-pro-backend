using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Infra.Data.Repositories.Interfaces;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IFireStoreRepository _fireStoreRepository;

        public FirebaseService(IFireStoreRepository fireStoreRepository) 
            => _fireStoreRepository = fireStoreRepository;

        public async Task<Result<string>> CreateAsync<T>(string collectionName, T document) where T : class
        {
            return await _fireStoreRepository.AddDoc(collectionName, document);
        }

        public async Task<Result> DeleteAsync(string collectionName, string documentId)
        {
            return await _fireStoreRepository.DeleteDoc(collectionName, documentId);
        }

        public async Task<Result> UpdateAsync<T>(string collectionName, string documentId, T document) where T : class
        {
            var result = await _fireStoreRepository.UpdateDoc(collectionName, documentId, document);
            if (!result.IsSuccess)
                return result.Exception;

            return result;
        }

        public async Task<T?> GetByIdAsync<T>(string collectionName, string documentId) where T : class
        {
            return await _fireStoreRepository.GetDocById<T>(collectionName, documentId);
        }
    }
}
