using ResponsibleSystem.Common.Config;

namespace ResponsibleSystem.Common.Azure.Storage.Tables
{
    public class TableStorageServiceConfig : IConfig
    {
        /// <summary>
        /// Connection string to cloud storage
        /// </summary>
        public string AzureWebJobsStorage { get; set; }
        public string TableName { get; set; }
    }
}
