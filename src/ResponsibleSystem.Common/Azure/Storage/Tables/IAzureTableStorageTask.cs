using Microsoft.WindowsAzure.Storage.Table;

namespace ResponsibleSystem.Common.Azure.Storage.Tables
{
    public interface ITableStorageTask<TEntity> where TEntity : ITableEntity, new()
    {
        TableQuery<TEntity> GetQuery();
        TEntity ExecuteTask(TEntity item);
    }
}
