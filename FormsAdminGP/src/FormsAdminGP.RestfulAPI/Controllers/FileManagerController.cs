using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-filemanager")]
    public class FileManagerController: BaseController
    {

        private readonly IOptions<AppSettings> _appSettings;
        public FileManagerController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet("download")]
        public async Task<FileResult> Download()
        {
            //var fileName = "AngularJS.pdf";
            ////var blockBlob = _blobUtility.DownloadBlob("cccc", "PX08-99-1806-Q7", fileName);
            //var fileInfo = new FileInfo("");
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    //await blockBlob.DownloadToStreamAsync(memoryStream);
            //    return File(memoryStream.ToArray(), "application/octet-stream", fileName);
            //}

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(@"C:\Users\GPertiDev\Documents\5345tttt.txt");
            string fileName = "5345tttt.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        [IgnoreAntiforgeryToken]
        public ActionResult Upload()
        {
            var response = new BaseResponse();
            try
            {
                var file = Request.Form.Files[0];
                var baseDir = _appSettings?.Value?.EbookPath ?? string.Empty;                
                if (!Directory.Exists(baseDir))
                {
                    Directory.CreateDirectory(baseDir);
                }
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(baseDir, fileName);
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
