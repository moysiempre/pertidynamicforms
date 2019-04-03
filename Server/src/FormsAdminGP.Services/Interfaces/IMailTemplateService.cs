using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.Interfaces
{
    public interface IMailTemplateService
    {
        Task<IEnumerable<MailTemplateDto>> GetAllAsync();
        Task<MailTemplateDto> GetByIdAsync(string id);      
        Task<BaseResponse> AddOrUpdateAsync(MailTemplateDto mailTemplateDto, string userId);      
        Task<BaseResponse> DeleteAsync(string id);
    }
}
