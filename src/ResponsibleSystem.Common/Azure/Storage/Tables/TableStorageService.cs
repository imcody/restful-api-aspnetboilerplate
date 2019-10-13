using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Common.Logs;

namespace ResponsibleSystem.Common.Azure.Storage.Tables
{
    public class TableStorageService : ITableStorageService
    {
        protected CloudTable CloudTable;

        public TableStorageService(IConfigFactory<TableStorageServiceConfig> configFactory)
        {
            var config = configFactory.GetConfig();
            ChangeTable(config).Wait();
        }

        public async Task<bool> ChangeTable(TableStorageServiceConfig config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.AzureWebJobsStorage);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable = tableClient.GetTableReference(config.TableName);

            return await CloudTable.CreateIfNotExistsAsync();
        }

        public async Task<TableResult> Insert(TableEntity entity)
        {
            TableOperation insertOperation = TableOperation.Insert(entity);
            return await CloudTable.ExecuteAsync(insertOperation);
        }

        public async Task<TableResult> InsertOrReplace(TableEntity entity)
        {
            TableOperation insertOperation = TableOperation.InsertOrReplace(entity);
            return await CloudTable.ExecuteAsync(insertOperation);
        }

        public async Task<IList<TableResult>> Insert(IList<TableEntity> entites)
        {
            TableBatchOperation batchOperation = new TableBatchOperation();
            entites.ToList().ForEach(e => batchOperation.Insert(e));
            return await CloudTable.ExecuteBatchAsync(batchOperation);
        }

        public async Task<IList<TableResult>> InsertOrReplace(IList<TableEntity> entites)
        {
            TableBatchOperation batchOperation = new TableBatchOperation();
            entites.ToList().ForEach(e => batchOperation.InsertOrReplace(e));
            return await CloudTable.ExecuteBatchAsync(batchOperation);
        }

        public virtual IList<T> ExecuteQuery<T>(TableQuery<T> query) where T : ITableEntity, new()
        {
            var result = new List<T>();

            TableContinuationToken continuationToken = null;
            do
            {
                // Retrieve a segment (up to 1,000 entities).
                TableQuerySegment<T> tableQueryResult = CloudTable.ExecuteQuerySegmentedAsync(query, continuationToken).Result;

                result.AddRange(tableQueryResult.Results);
                continuationToken = tableQueryResult.ContinuationToken;

            } while (continuationToken != null);

            return result;
        }

        public async Task<bool> DeleteTable()
        {
            return await CloudTable.DeleteIfExistsAsync();
        }

        // TODO: add methods for
        // delete single record
        // Delete an entity
        // Retrieve entities in pages asynchronously
        /// <summary>
        /// MS docs - https://docs.microsoft.com/en-us/azure/cosmos-db/table-storage-how-to-use-dotnet#retrieve-entities-in-pages-asynchronously
        /// </summary>

        // TODO: update that
        //https://stackoverflow.com/questions/17955557/painfully-slow-azure-table-insert-and-delete-batch-operations
        public void BatchUpdate<TEntity>(IList<TEntity> data, ILogger logger = null) where TEntity : ITableEntity
        {
            var groupedByPartition = data.GroupBy(x => x.PartitionKey).ToList();

            foreach (var group in groupedByPartition)
            {
                var entities = group.Select(x => x).ToList();

                logger?.Info("Saving for PartitionKey = " + group.Key + $" ({entities.Count}) records");

                int rowOffset = 0;
                while (rowOffset < entities.Count)
                {
                    var rows = entities.Skip(rowOffset).Take(100).ToList();
                    rowOffset += rows.Count;

                    string partition = "$" + rowOffset.ToString();

                    var batch = new TableBatchOperation();
                    foreach (var row in rows)
                    {
                        batch.Replace(row);
                    }

                    CloudTable.ExecuteBatchAsync(batch);

                    logger?.Info("Updated batch for partition " + partition);
                }
            }
        }
    }
}
