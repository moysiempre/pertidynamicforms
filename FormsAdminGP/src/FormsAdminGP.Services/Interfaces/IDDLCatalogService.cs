using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.Interfaces
{
    public interface IDDLCatalogService
    {
        Task<IEnumerable<DDLCatalogDto>> GetAllAsync();
        Task<DDLCatalogDto> GetByIdAsync(string id);
        Task<BaseResponse> AddOrUpdateAsync(DDLCatalogDto catalogDto);
        Task<BaseResponse> DeleteAsync(string id);
    }
}
