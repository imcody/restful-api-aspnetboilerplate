using System.IO;
using System.Threading.Tasks;

namespace ResponsibleSystem.Common.Azure.Storage.Blob
{
    public interface IAzureBlobService
    {
        Task<string> UploadFile(string fileName, FileStream fileStream);
        string CreateBlobUrl(string assetId);
        Task<bool> DeleteBlobAsset(string assetId);
    }
}
