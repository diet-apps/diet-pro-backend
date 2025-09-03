using OperationResult;

namespace Diet.Pro.AI.Aplication.Interfaces
{
    public interface IFirebaseService
    {
        Task<Result<string>> CreateAsync<T>(string collectionName, T document) where T : class;
        Task<Result> UpdateAsync<T>(string collectionName, string documentId, T document) where T : class;
        Task<T?> GetByIdAsync<T>(string collectionName, string documentId) where T : class;
        Task<Result> DeleteAsync(string collectionName, string documentId);
    }
}
