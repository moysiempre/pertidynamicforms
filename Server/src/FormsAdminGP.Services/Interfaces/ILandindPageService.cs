using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Request;
using FormsAdminGP.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.Interfaces
{
    public interface ILandindPageService
    {
        Task<IEnumerable<LandingPageDto>> GetAllAsync();
        Task<IEnumerable<KeyValuePair<string, string>>> GetAllforDDL();
        Task<LandingPageDto> GetByIdAsync(string id);
        Task<IEnumerable<LandingPageDto>> GetAllOptionsAsync();
        Task<BaseResponse> AddOrUpdateAsync(LandingPageDto landingPageDto, string userId);
        Task<BaseResponse> DeleteAsync(string id, string userId);
    }
}
