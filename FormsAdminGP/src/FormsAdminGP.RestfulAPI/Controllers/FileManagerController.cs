using FormsAdminGP.Services.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-fileupload")]
    public class FileManagerController: BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileManagerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("downloadFilex")]
        public async Task<ActionResult> DownloadFileex()
        {
            var fileName = "AngularJS.pdf";
            //var blockBlob = _blobUtility.DownloadBlob("cccc", "PX08-99-1806-Q7", fileName);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //await blockBlob.DownloadToStreamAsync(memoryStream);
                return File(memoryStream.ToArray(), "application/octet-stream", fileName);
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [IgnoreAntiforgeryToken]
        public ActionResult UploadFilex()
        {
            var response = new BaseResponse();
            try
            {
                var file = Request.Form.Files[0];
                const string folderName = "EbookUploaded";
                var webRootPath = @"C:\Workspaces\temp"; // _hostingEnvironment.WebRootPath;
                var newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                response.Success = true;
                response.Message = "Upload Successful.";
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Message = "Upload Failed.";
            }

            return Ok(response);
        }
    }
}
