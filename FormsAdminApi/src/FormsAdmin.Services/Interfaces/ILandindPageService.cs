using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdmin.Services.Interfaces
{
    public interface ILandindPageService
    {
        Task<IEnumerable<LandingPageDto>> GetAllAsync();
        Task<LandingPageDto> GetByIdAsync(string id);
        Task<BaseResponse> AddOrUpdateAsync(LandingPageDto landingPageDto, string userId);
        Task<BaseResponse> DeleteAsync(string id, string userId);
    }
}
