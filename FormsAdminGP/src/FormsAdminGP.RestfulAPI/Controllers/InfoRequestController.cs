using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-inforequest")]
    [AllowAnonymous]
    public class InfoRequestController: Controller
    {
        private readonly IInfoRequestService _infoRequestService;
        public InfoRequestController(IInfoRequestService infoRequestService)
        {
            _infoRequestService = infoRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _infoRequestService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("getby")]
        public async Task<IActionResult> GetByAsync(BaseRequest request)
        {
            var list = await _infoRequestService.GetByAsync(request);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _infoRequestService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post([FromBody]InfoRequestDto infoRequestDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _infoRequestService.AddAsync(infoRequestDto);
                return Ok(item);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _infoRequestService.DeleteAsync(id);
            return Ok(item);
        }
    }
}
