using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResponsibleSystem.Common.CosmosDb.Repositories
{
    public interface ICosmosDbRepository<T>
    {
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetItemsAsync(string sqlQueryExpression);
        Task<IEnumerable<T>> GetItemsAsync(string sqlQueryExpression, IDictionary<string, object> parameters);

        // Limit select fields with proper mappings

        Task<IEnumerable<T>> GetAllAsync<TDto>();
        Task<IEnumerable<T>> GetItemsAsync<TDto>(string sqlQueryExpression);
        Task<IEnumerable<T>> GetItemsAsync<TDto>(string sqlQueryExpression, IDictionary<string, object> parameters);

        Task<Document> CreateItemAsync(T item);
        Task<Document> UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id);
    }
}