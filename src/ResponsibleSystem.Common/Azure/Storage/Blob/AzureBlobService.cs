using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Config;
using ResponsibleSystem.Exceptions;

namespace ResponsibleSystem.Common.Azure.Storage.Blob
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly AzureBlobConfig _config;

        public AzureBlobService(IConfigFactory<AzureBlobConfig> configFactory)
        {
            _config = configFactory.GetConfig();
        }

        public async Task<string> UploadFile(string fileName, FileStream fileStream)
        {
            var container = GetBlobContainer();
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            var assetId = $"{fileName}";
            var blockBlob = container.GetBlockBlobReference(assetId);

            await blockBlob.UploadFromStreamAsync(fileStream);

            return blockBlob.Uri.AbsoluteUri;
        }

        public string CreateBlobUrl(string assetId)
        {
            var storageAccount = CloudStorageAccount.Parse(_config.AzureWebJobsStorage);
            return $"{storageAccount.BlobStorageUri.PrimaryUri}{_config.ContainerName}/{assetId}";
        }

        public async Task<bool> DeleteBlobAsset(string assetId)
        {
            var container = GetBlobContainer();

            var blockBlob = container.GetBlockBlobReference(assetId);
            return await blockBlob.DeleteIfExistsAsync();
        }

        private CloudBlobContainer GetBlobContainer()
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(_config.AzureWebJobsStorage);
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(_config.ContainerName);
                return container;
            }
            catch (Exception ex)
            {
                var fatalExcepion = new CriticalException("Error while connecting to Azure Blob Service", ex);
                throw fatalExcepion;
            }
        }
    }
}
