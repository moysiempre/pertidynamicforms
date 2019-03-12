using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-forms")]
    [Authorize]
    public class FormHdController: Controller
    {
        private readonly IFormHdService _formHdService;
        public FormHdController(IFormHdService formHdService)
        {
            _formHdService = formHdService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _formHdService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _formHdService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post([FromBody]FormHdDto formHdDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _formHdService.AddOrUpdateAsync(formHdDto);
                return Ok(item);
            }

            return NoContent();           
        }

        [HttpPost("detail")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> PostDetail([FromBody]FormDetailDto formDetailDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _formHdService.AddOrUpdateDetailAsync(formDetailDto);
                return Ok(item);
            }

            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _formHdService.DeleteAsync(id);
            return Ok(item);
        }
    }
}
