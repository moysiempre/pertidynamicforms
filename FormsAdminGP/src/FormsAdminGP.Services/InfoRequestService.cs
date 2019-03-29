using AutoMapper;
using FormsAdminGP.Common.Enums;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Core.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.EmailSender;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Request;
using FormsAdminGP.Services.Responses;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class InfoRequestService: IInfoRequestService
    {
        private readonly IInfoRequestRepository _infoRequestRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IMapper _mapper;
 
        public InfoRequestService(
            IInfoRequestRepository infoRequestRepository,
            IEmailSenderService emailSenderService,
            IMailTemplateService mailTemplateService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _infoRequestRepository = infoRequestRepository;
            _emailSenderService = emailSenderService;
            _mailTemplateService = mailTemplateService;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InfoRequestDto>> GetAllAsync()
        {
            var listDto = new List<InfoRequestDto>();
            try
            {
                var list = await _infoRequestRepository.GetAll();
                listDto = _mapper.Map<List<InfoRequestDto>>(list);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return listDto;
        }

        public async Task<IEnumerable<InfoRequestDto>> GetByAsync(BaseRequest request)
        {
            var listDto = new List<InfoRequestDto>();
            try
            {
                var list = await _infoRequestRepository.FindBy(x => x.IsActive, x => x.LandingPage);
                if (!string.IsNullOrEmpty(request.LandingPageId))
                {
                    list = list.Where(x => x.LandingPageId == request.LandingPageId);
                }

                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    list = list.Where(x => x.RequestDate >= request.StartDate && x.RequestDate <= request.EndDate);
                }
                else
                {
                    if (request.StartDate.HasValue)
                    {
                        list = list.Where(x => x.RequestDate >= request.StartDate);
                    }

                    if (request.EndDate.HasValue)
                    {
                        list = list.Where(x => x.RequestDate <= request.EndDate);
                    }
                }

                listDto = _mapper.Map<List<InfoRequestDto>>(list);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return listDto;
        }

        public async Task<InfoRequestDto> GetByIdAsync(string id)
        {
            InfoRequestDto itemDto = null;
            try
            {
                var item = await _infoRequestRepository.FindEntityBy(x => x.Id == id);
                itemDto = _mapper.Map<InfoRequestDto>(item);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return itemDto;
        }

        public async Task<BaseResponse> AddAsync(InfoRequestDto infoRequestDto)
        {
            var response = new BaseResponse();
            try
            {                
                var infoRequest = _mapper.Map<InfoRequest>(infoRequestDto);
                if (string.IsNullOrEmpty(infoRequest.Id))
                {
                    infoRequest.Id = Common.Utilities.Utils.NewGuid;
                    infoRequest.IsActive = true;
                    infoRequest.RequestDate = DateTime.UtcNow;
                    _infoRequestRepository.Add(infoRequest);

                    var item = await _infoRequestRepository.SaveChanges();
                    var attach = string.Empty;
                    if (!string.IsNullOrEmpty(infoRequestDto.FileName))
                    {
                        var baseDir = _appSettings?.Value?.EbookPath ?? string.Empty;
                        attach = Path.Combine(baseDir, infoRequestDto.FileName);                       
                    }

                    await SendMailToClient(infoRequestDto.Email, infoRequestDto.Name, attach, infoRequestDto.MailTemplateId);

                    response.Success = true;
                    response.Id = infoRequest.Id;
                    response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;

                }
                else
                {
                    response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                }

                
                
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
                var item = await _infoRequestRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                    return response;
                }

                _infoRequestRepository.Delete(item);
                await _infoRequestRepository.SaveChanges();
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

        private async Task SendMailToClient(string email, string name, string attach, string mailTemplateId)
        {
            var emails = new List<KeyValuePair<string, WithEMail>>();
            var attachs = new List<string>();
            if(!string.IsNullOrEmpty(attach))
            {
                attachs.Add(attach);
            };
            name = name ?? email;
            emails.Add(new KeyValuePair<string, WithEMail>(email, WithEMail.To));

            var mailTemplate = await _mailTemplateService.GetByIdAsync(mailTemplateId);
            var subject = "Mensaje de notificación";
            var message = $"<div><p>Hola {name}</p><p>¡Gracias por tu interés en Grupo PerTI!</p><p></p>Nos pondremos en contacto contigo lo más pronto posible.<div>";
            if(mailTemplate != null)
            {
                subject = mailTemplate.Subject;
                message = mailTemplate.Body;
            }

            try
            {
                await _emailSenderService.SendEmailAsync(emails, subject, message, attachs);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }
        }
         
    }
}
