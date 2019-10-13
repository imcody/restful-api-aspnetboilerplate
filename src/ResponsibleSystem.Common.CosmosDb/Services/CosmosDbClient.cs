using ResponsibleSystem.Common.CosmosDb.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Exceptions;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus;

namespace ResponsibleSystem.Common.CosmosDb.Services
{
    public class CosmosDbClient : ICosmosDbClient
    {
        protected DocumentClient Client { get; private set; }
        protected string DatabaseId { get; private set; }
        protected string DefaultCollectionId { get; set; }

        protected Uri CollectionLink(string collectionId) => UriFactory.CreateDocumentCollectionUri(DatabaseId, string.IsNullOrWhiteSpace(collectionId) ? DefaultCollectionId : collectionId);
        protected Uri DocumentLink(string collectionId, string id) => UriFactory.CreateDocumentUri(DatabaseId, string.IsNullOrWhiteSpace(collectionId) ? DefaultCollectionId : collectionId, id);

        public virtual void Initialize(IConfigFactory<CosmosDbConfig> configFactory)
        {
            try
            {
                var config = configFactory.GetConfig();
                var key = config.MasterKey;
                var endpoint = config.Endpoint;
                DatabaseId = config.DatabaseId;
                DefaultCollectionId = string.IsNullOrWhiteSpace(config.DefaultCollectionId) ? DatabaseId : config.DefaultCollectionId;

                Client = new DocumentClient(new Uri(endpoint), key, new ConnectionPolicy
                {
                    EnableEndpointDiscovery = false
                });
                Client.OpenAsync();

                CreateDatabaseIfNotExistsAsync().Wait();
                CreateCollectionIfNotExistsAsync(DefaultCollectionId).Wait();
            }
            catch (AggregateException ex)
            {
                TriggerCriticalExceptonEvent(ex.InnerException);
                throw;
            }
            catch (Exception ex)
            {
                TriggerCriticalExceptonEvent(ex);
                throw;
            }
        }

        public virtual async Task<T> GetItemAsync<T>(string collectionId, string id)
        {
            try
            {
                Document document = await Client.ReadDocumentAsync(DocumentLink(collectionId, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(T);
                }
                else
                {
                    throw;
                }
            }
        }

        public Task<int> GetCount(string sqlQueryExpression)
        {
            return GetCount(sqlQueryExpression, null);
        }

        public async Task<int> GetCount(string sqlQueryExpression, IDictionary<string, object> parameters)
        {
            return (await GetItemsAsync<int>(sqlQueryExpression, null)).FirstOrDefault();
        }

        public virtual async Task<Document> CreateItemAsync<T>(string collectionId, T item)
        {
            return await Client.CreateDocumentAsync(CollectionLink(collectionId), item);
        }

        public virtual async Task<Document> UpdateItemAsync<T>(string collectionId, string id, T item)
        {
            return await Client.ReplaceDocumentAsync(DocumentLink(collectionId, id), item);
        }

        public virtual async Task<Document> UpsertItemAsync<T>(string collectionId, string id, T item)
        {
            return await Client.UpsertDocumentAsync(CollectionLink(collectionId), item);
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync<T>(string sqlQueryExpression)
        {
            return GetItemsAsync<T>(null, sqlQueryExpression, null);
        }

        public virtual Task<IEnumerable<T>> GetItemsAsync<T>(string sqlQueryExpression, IDictionary<string, object> parameters)
        {
            return GetItemsAsync<T>(null, sqlQueryExpression, parameters);
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync<T>(string collectionId, string sqlQueryExpression, IDictionary<string, object> parameters)
        {
            var parametersCollection = new SqlParameterCollection();
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    parametersCollection.Add(new SqlParameter($"@{item.Key}", item.Value));
                }
            }

            var sqlQuerySpec = new SqlQuerySpec()
            {
                QueryText = sqlQueryExpression,
                Parameters = parametersCollection
            };

            IDocumentQuery<T> query = Client.CreateDocumentQuery<T>(
                    CollectionLink(collectionId),
                    sqlQuerySpec,
                    new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public virtual Task DeleteItemAsync(string collectionId, string id)
        {
            return Client.DeleteDocumentAsync(DocumentLink(collectionId, id));
        }

        protected virtual async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        public virtual async Task CreateCollectionIfNotExistsAsync(string collectionId)
        {
            try
            {
                await Client.ReadDocumentCollectionAsync(CollectionLink(collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = collectionId },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
        
        private static void TriggerCriticalExceptonEvent(Exception ex)
        {
            var criticalException = new CriticalException(ex.InnerException.Message, ex.InnerException);
            EventBus.Default.Trigger(new AbpHandledExceptionData(criticalException));
        }
    }
}
