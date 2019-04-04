using AutoMapper;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly IMapper _mapper;
        public MailTemplateService(
            IMailTemplateRepository mailTemplateRepository,
            IMapper mapper)
        {
            _mailTemplateRepository = mailTemplateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MailTemplateDto>> GetAllAsync()
        {
            var listDto = new List<MailTemplateDto>();
            try
            {
                var list = await _mailTemplateRepository.GetAll();
                listDto = _mapper.Map<List<MailTemplateDto>>(list);
            }
            catch (System.Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return listDto;
        }

        public async Task<MailTemplateDto> GetByIdAsync(string id)
        {
            MailTemplateDto itemDto = null;
            try
            {
                var item = await _mailTemplateRepository.FindEntityAsNoTrackingBy(x => x.Id == id);
                itemDto = _mapper.Map<MailTemplateDto>(item);
            }
            catch (System.Exception ex)
            {
                LoggerService.LogToFile(ex);
            }
            
            return itemDto;
        }

        public async Task<BaseResponse> AddOrUpdateAsync(MailTemplateDto mailTemplateDto, string userName)
        {
            var response = new BaseResponse();
            try
            {
                _mailTemplateRepository.UserName = userName;
                var mailTemplate = _mapper.Map<MailTemplate>(mailTemplateDto);
                if (string.IsNullOrEmpty(mailTemplate.Id))
                {
                    //validar el nombre del template page
                    var template = await _mailTemplateRepository.FindEntityBy(x => x.Name.Trim().ToLower() == mailTemplateDto.Name.Trim().ToLower());
                    if (template != null)
                    {
                        response.Message = LoggingEvents.INSERT_DUPLICATED_MESSAGE;
                        return response;
                    }


                    mailTemplate.Id = Common.Utilities.Utils.NewGuid;
                    mailTemplate.IsActive = true;
                    _mailTemplateRepository.Add(mailTemplate);
                    response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
                }
                else
                {
                    //validar el nombre del template page
                    var template = await _mailTemplateRepository.FindEntityBy(x => x.Name.Trim().ToLower() == mailTemplateDto.Name.Trim().ToLower() && x.Id != mailTemplateDto.Id);
                    if (template != null)
                    {
                        response.Message = LoggingEvents.INSERT_DUPLICATED_MESSAGE;
                        return response;
                    }

                    _mailTemplateRepository.Edit(mailTemplate);
                    response.Message = LoggingEvents.UPDATE_SUCCESS_MESSAGE;
                }

                var item = await _mailTemplateRepository.SaveChanges();
                response.Success = true;
                response.Id = mailTemplate.Id;
                

            }
            catch (System.Exception ex)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                LoggerService.LogToFile(ex);
            }

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                var item = await _mailTemplateRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                    return response;
                }

                _mailTemplateRepository.Delete(item);
                await _mailTemplateRepository.SaveChanges();
                response.Success = true;
                response.Message = LoggingEvents.DELETE_SUCCESS_MESSAGE;

            }
            catch (System.Exception ex)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                LoggerService.LogToFile(ex);
            }

            return response;
        }

        
    }
}
