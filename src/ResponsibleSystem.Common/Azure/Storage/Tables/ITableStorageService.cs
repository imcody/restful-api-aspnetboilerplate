using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Logs;

namespace ResponsibleSystem.Common.Azure.Storage.Tables
{
    public interface ITableStorageService
    {
        Task<bool> ChangeTable(TableStorageServiceConfig config);
        Task<TableResult> Insert(TableEntity entity);
        Task<TableResult> InsertOrReplace(TableEntity entity);
        Task<IList<TableResult>> Insert(IList<TableEntity> entites);
        Task<IList<TableResult>> InsertOrReplace(IList<TableEntity> entites);
        IList<T> ExecuteQuery<T>(TableQuery<T> query) where T : ITableEntity, new();
        Task<bool> DeleteTable();
        void BatchUpdate<TEntity>(IList<TEntity> data, ILogger logger = null) where TEntity : ITableEntity;
    }
}
