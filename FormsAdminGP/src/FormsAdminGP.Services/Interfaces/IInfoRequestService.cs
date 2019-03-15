using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.Interfaces
{
    public interface IInfoRequestService
    {
        Task<IEnumerable<InfoRequestDto>> GetAllAsync();
        Task<InfoRequestDto> GetByIdAsync(string id);
        Task<BaseResponse> AddAsync(InfoRequestDto infoRequestDto);
        Task<BaseResponse> DeleteAsync(string id);
    }
}
