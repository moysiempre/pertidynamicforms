using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-landingpage")]
    [Authorize]
    public class LandingPageController: BaseController
    {

        private readonly ILandindPageService _landindPageService;
        public LandingPageController(ILandindPageService landindPageService)
        {
            _landindPageService = landindPageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _landindPageService.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _landindPageService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpGet("options")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllOptionsAsync()
        {
            var list = await _landindPageService.GetAllOptionsAsync();
            return Ok(list);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post([FromBody]LandingPageDto landingPageDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _landindPageService.AddOrUpdateAsync(landingPageDto, CurrentUser.UserId);
                return Ok(item);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _landindPageService.DeleteAsync(id, CurrentUser.UserId);
            return Ok(item);
        }
    }
}
