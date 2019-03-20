using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-catalog")]
    public class CatalogController : BaseController
    {
        private readonly IDDLCatalogService _catalogService;
        public CatalogController(IDDLCatalogService catalogService)
        {
            _catalogService = catalogService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _catalogService.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _catalogService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post([FromBody]DDLCatalogDto catalogDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _catalogService.AddOrUpdateAsync(catalogDto);
                return Ok(item);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _catalogService.DeleteAsync(id);
            return Ok(item);
        }

    }
}
