using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.Interfaces
{
    public interface IFormHdService
    {
        Task<IEnumerable<FormHdDto>> GetAllAsync();
        Task<FormHdDto> GetByIdAsync(string id);
        Task<FormHdDto> GetByLandingPageIdAsync(string landingPageId);
        Task<IEnumerable<FormHdDto>> GetAllForOptionsAsync();
        Task<BaseResponse> AddOrUpdateAsync(FormHdDto formHdDto, string userId);
        Task<BaseResponse> UpdateFormHdFileAsync(string id, string fileName);
        Task<BaseResponse> DeleteAsync(string id);

        Task<IEnumerable<FormDetailDto>> GetDetailAllAsync(string formHdId);
        Task<FormDetailDto> GetDetailByIdAsync(string id);
        Task<IEnumerable<BaseDetailSettings>> GetBaseDetail();
        Task<BaseResponse> AddOrUpdateDetailAsync(FormDetailDto formDetailDto);
        Task<BaseResponse> DeleteDetailAsync(string id);
    }
}
