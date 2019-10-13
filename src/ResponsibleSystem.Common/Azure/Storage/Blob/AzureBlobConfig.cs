using ResponsibleSystem.Common.Config;

namespace ResponsibleSystem.Common.Azure.Storage.Blob
{
    [ConfigBasePath("App:AzureBlobConfig")]
    public class AzureBlobConfig : IConfig
    {
        public string AzureWebJobsStorage { get; set; }
        public string BaseUrl { get; set; }
        public string ContainerName { get; set; }
    }
}
