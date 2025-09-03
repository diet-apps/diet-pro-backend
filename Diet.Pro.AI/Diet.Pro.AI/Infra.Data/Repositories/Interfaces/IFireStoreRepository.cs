using OperationResult;

namespace Diet.Pro.AI.Infra.Data.Repositories.Interfaces
{
    public interface IFireStoreRepository
    {
        Task<Result<string>> AddDoc<T>(string collectionName, T document) where T : class;
        Task<Result> DeleteDoc(string collectionName, string documentId);
        Task<Result> UpdateDoc<T>(string collectionName, string documentId, T document) where T : class;
        Task<T?> GetDocById<T>(string collectionName, string documentId) where T : class;
    }
}
