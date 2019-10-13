using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResponsibleSystem.Controllers;
using Microsoft.AspNetCore.Http;
using ResponsibleSystem.Exceptions;
using ResponsibleSystem.Common.Azure.Storage.Blob;

namespace ResponsibleSystem.Web.Host.Controllers
{
    public class HomeController : ResponsibleSystemControllerBase
    {
        private readonly IAzureBlobService _azureBlobService;

        public HomeController(IAzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        [HttpPost]
        [Route("/api/upload")]
        public async Task<string> UploadFiles(List<IFormFile> files)
        {
            if (files.Count < 1)
                return "";

            var formFile = files[0];
            var filePath = Path.GetTempFileName();
            if (formFile.Length <= 0)
                throw new ResponsibleSystemUserFriendlyException($"No files has been send", "No file have been send in request");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var assetId = await _azureBlobService.UploadFile(formFile.FileName, stream);
                return assetId;
            }
        }
    }
}
