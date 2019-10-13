using ResponsibleSystem.Common.Config;

namespace ResponsibleSystem.Common.CosmosDb.Repositories
{
    [ConfigBasePath("App:CosmosDbConfig")]
    public class CosmosDbConfig : IConfig
    {
        public string Endpoint { get; set; }
        public string MasterKey { get; set; }
        public string DatabaseId { get; set; }
        public string DefaultCollectionId { get; set; }
    }
}