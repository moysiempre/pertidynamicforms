using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsAdmin.Services.Interfaces
{
    public interface IFormHdService
    {
        Task<IEnumerable<FormHdDto>> GetAllAsync();
        Task<FormHdDto> GetByIdAsync(string id);
        Task<BaseResponse> AddOrUpdateAsync(FormHdDto formHdDto);
        Task<BaseResponse> DeleteAsync(string id);

        Task<IEnumerable<FormDetailDto>> GetDetailAllAsync(string formHdId);
        Task<FormDetailDto> GetDetailByIdAsync(string id);
        Task<BaseResponse> AddOrUpdateDetailAsync(FormDetailDto formDetailDto);
        Task<BaseResponse> DeleteDetailAsync(string id);
    }
}
