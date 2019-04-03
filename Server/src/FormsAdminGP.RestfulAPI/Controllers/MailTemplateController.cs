using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{


    [Produces("application/json")]
    [Route("landing/api-mailtemplate")]
    public class MailTemplateController : BaseController
    {
        private readonly IMailTemplateService _mailTemplateService;
        public MailTemplateController(IMailTemplateService mailTemplateService)
        {
            _mailTemplateService = mailTemplateService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _mailTemplateService.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _mailTemplateService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post([FromBody]MailTemplateDto templateDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _mailTemplateService.AddOrUpdateAsync(templateDto, CurrentUser.UserName);
                return Ok(item);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _mailTemplateService.DeleteAsync(id);
            return Ok(item);
        }

    }
}
