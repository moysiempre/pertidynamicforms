using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using Microsoft.Extensions.Logging;

namespace FormsAdminGP.Services
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public MailTemplateService(
            IMailTemplateRepository mailTemplateRepository,
            ILogger<MailTemplateService> logger,
            IMapper mapper)
        {
            _mailTemplateRepository = mailTemplateRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MailTemplateDto>> GetAllAsync()
        {
            var list = await _mailTemplateRepository.GetAll();
            return _mapper.Map<List<MailTemplateDto>>(list);
        }

        public async Task<MailTemplateDto> GetByIdAsync(string id)
        {
            var item = await _mailTemplateRepository.FindBy(x=>x.Id == id);
            return _mapper.Map<MailTemplateDto>(item);
        }

        public async Task<BaseResponse> AddOrUpdateAsync(MailTemplateDto mailTemplateDto)
        {
            var response = new BaseResponse();
            try
            {
                var mailTemplate = _mapper.Map<MailTemplate>(mailTemplateDto);
                if (string.IsNullOrEmpty(mailTemplate.Id))
                {
                    mailTemplate.Id = Common.Utilities.Utils.NewGuid;
                    mailTemplate.IsActive = true;
                    _mailTemplateRepository.Add(mailTemplate);
                }
                else
                {
                    mailTemplate = await _mailTemplateRepository.FindEntityBy(x => x.Id == mailTemplateDto.Id);
                    if (mailTemplate != null)
                    {
                        _mailTemplateRepository.Edit(mailTemplate);
                    }
                    else
                    {

                        response.Message = LoggingEvents.UPDATE_FAILED_MESSAGE;
                        return response;
                    }


                }

                var item = await _mailTemplateRepository.SaveChanges();

                response.Success = true;
                response.Id = mailTemplate.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;

            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger 
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
            catch (System.Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger
            }

            return response;
        }

        
    }
}
