using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdmin.Services.Interfaces
{
    public interface IInfoRequestService
    {
        Task<IEnumerable<InfoRequestDto>> GetAllAsync();
        Task<InfoRequestDto> GetByIdAsync(string id);
        Task<BaseResponse> AddOrUpdateAsync(InfoRequestDto infoRequestDto);
        Task<BaseResponse> DeleteAsync(string id);
    }
}
