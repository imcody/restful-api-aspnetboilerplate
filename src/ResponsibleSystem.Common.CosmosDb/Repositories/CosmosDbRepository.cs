using ResponsibleSystem.Common.CosmosDb.Services;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResponsibleSystem.Common.CosmosDb.Domain;

namespace ResponsibleSystem.Common.CosmosDb.Repositories
{
    public class CosmosDbRepository<T> : ICosmosDbRepository<T>
        where T : CosmosDbEntityBase
    {
        // use default collection if not specified
        protected virtual string CollectionId => null;

        private readonly ICosmosDbClient _client;
        private readonly CosmoDbSqlQueryBuilder _queryBuilder;

        public CosmosDbRepository(ICosmosDbClient client)
        {
            _client = client;
            _queryBuilder = new CosmoDbSqlQueryBuilder();
        }

        public virtual Task<T> GetItemAsync(string id)
        {
            return _client.GetItemAsync<T>(CollectionId, id);
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return GetItemsAsync("SELECT * FROM c");
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync(string sqlQueryExpression)
        {
            return GetItemsAsync(sqlQueryExpression, null);
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync(string sqlQueryExpression, IDictionary<string, object> parameters)
        {
            var sqlQuery = _queryBuilder.GetSelectStatement(sqlQueryExpression);
            sqlQuery = _queryBuilder.GetWhereConditional(sqlQuery, typeof(T), CollectionId);

            return _client.GetItemsAsync<T>(CollectionId, sqlQuery.ToString(), parameters);
        }

        public virtual Task<IEnumerable<T>> GetAllAsync<TDto>()
        {
            return GetItemsAsync<TDto>(string.Empty, null);
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync<TDto>(string sqlQueryExpression)
        {
            return GetItemsAsync<TDto>(sqlQueryExpression, null);
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync<TDto>(string sqlQueryExpression, IDictionary<string, object> parameters)
        {
            var sqlQuery = _queryBuilder.GetSelectStatement(typeof(TDto));
            if (!string.IsNullOrWhiteSpace(sqlQueryExpression))
                sqlQuery.Append($" WHERE {sqlQueryExpression}");

            return GetItemsAsync(sqlQuery.ToString(), parameters);
        }

        public virtual Task<Document> CreateItemAsync(T item)
        {
            return _client.CreateItemAsync(CollectionId, item);
        }

        public virtual Task<Document> UpdateItemAsync(string id, T item)
        {
            return _client.UpdateItemAsync(CollectionId, id, item);
        }

        public virtual Task<Document> UpdateItemAsync(T item)
        {
            return _client.UpdateItemAsync(CollectionId, item.Id, item);
        }

        public virtual Task<Document> CreateOrUpdateItemAsync(string id, T item)
        {
            return _client.UpsertItemAsync(CollectionId, id, item);
        }

        public virtual Task<Document> CreateOrUpdateItemAsync(T item)
        {
            return _client.UpsertItemAsync(CollectionId, item.Id, item);
        }

        public virtual Task DeleteItemAsync(string id)
        {
            return _client.DeleteItemAsync(CollectionId, id);
        }
    }
}