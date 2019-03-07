using FormsAdmin.Core.DTO;
using FormsAdmin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormsAdmin.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-inforequest")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _infoRequestService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InfoRequestDto infoRequestDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _infoRequestService.AddOrUpdateAsync(infoRequestDto);
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
