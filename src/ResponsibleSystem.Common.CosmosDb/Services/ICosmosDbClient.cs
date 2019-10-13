using ResponsibleSystem.Common.CosmosDb.Repositories;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Config;

namespace ResponsibleSystem.Common.CosmosDb.Services
{
    public interface ICosmosDbClient
    {
        Task<T> GetItemAsync<T>(string collectionId, string id);
        Task<IEnumerable<T>> GetItemsAsync<T>(string sqlQueryExpression);
        Task<IEnumerable<T>> GetItemsAsync<T>(string sqlQueryExpression, IDictionary<string, object> parameters);
        Task<IEnumerable<T>> GetItemsAsync<T>(string collectionId, string sqlQueryExpression, IDictionary<string, object> parameters);
        Task<Document> CreateItemAsync<T>(string collectionId, T item);
        Task<Document> UpdateItemAsync<T>(string collectionId, string id, T item);
        Task<Document> UpsertItemAsync<T>(string collectionId, string id, T item);
        Task DeleteItemAsync(string collectionId, string id);
        Task<int> GetCount(string sqlQueryExpression);
        Task<int> GetCount(string sqlQueryExpression, IDictionary<string, object> parameters);

        void Initialize(IConfigFactory<CosmosDbConfig> configFactory);
        Task CreateCollectionIfNotExistsAsync(string collectionId);
    }
}
