using FormsAdminGP.Common.Events;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
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
        private readonly IFormHdService _formHdService;       
        public FileManagerController(IOptions<AppSettings> appSettings, IFormHdService formHdService)
        {
            _appSettings = appSettings;
            _formHdService = formHdService;
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        { 
            var baseDir = _appSettings?.Value?.EbookPath ?? string.Empty;
            var fullPath = Path.Combine(baseDir, fileName);
            byte[] fileBytes = null;
            if (System.IO.File.Exists(fullPath)){
                fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> Upload()
        {
            var response = new BaseResponse();
            try
            {
                var file = Request.Form.Files[0];
                string formHdId = Request.Form["formHdId"];
                formHdId = formHdId ?? formHdId.ToString();
                var baseDir = _appSettings?.Value?.EbookPath ?? string.Empty;
                var fileName = string.Empty;
                if (!Directory.Exists(baseDir))
                {
                    Directory.CreateDirectory(baseDir);
                }
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(baseDir, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                response = await UpdateFormHdFileAsync(formHdId, fileName);
                response.Message = LoggingEvents.UPLOAD_FILE_SUCCESS_MESSAGE;

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Message = LoggingEvents.UPLOAD_FILE_FAILED_MESSAGE;
            }

            return Ok(response);
        }

        [HttpDelete("remove/{formHdId}/{fileName}")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> Remove(string formHdId,  string fileName)
        {
            var baseDir = _appSettings?.Value?.EbookPath ?? string.Empty;
            var file = new FileInfo(Path.Combine(baseDir, fileName));
            if (file.Exists)
            {
                file.Delete();
            }

           var response = await UpdateFormHdFileAsync(formHdId, string.Empty);
            if (response.Success)
            {
                response.Message = LoggingEvents.REMOVE_FILE_SUCCESS_MESSAGE;
            }
            else
            {
                response.Message = LoggingEvents.REMOVE_FILE_FAILED_MESSAGE;
            }
            return Ok(response);
        }


        private async Task<BaseResponse> UpdateFormHdFileAsync(string formHdId, string fileName)
        {
            var response = new BaseResponse();
            if (string.IsNullOrEmpty(formHdId))
            {
                response.Message = LoggingEvents.REMOVE_FILE_FAILED_MESSAGE;
            }
            else
            {
                var res = await _formHdService.UpdateFormHdFileAsync(formHdId, fileName);
                response.Success = res.Success;              
            }

            return response;
        }

    }
}
