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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class InfoRequestService: IInfoRequestService
    {
        private readonly IInfoRequestRepository _infoRequestRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;
        public InfoRequestService(
            IInfoRequestRepository infoRequestRepository,
            IEmailSenderService emailSenderService,
            IMapper mapper)
        {
            _infoRequestRepository = infoRequestRepository;
            _emailSenderService = emailSenderService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InfoRequestDto>> GetAllAsync()
        {
            var list = await _infoRequestRepository.GetAll();
            return _mapper.Map<List<InfoRequestDto>>(list);  
        }

        public async Task<IEnumerable<InfoRequestDto>> GetByAsync(BaseRequest request)
        {
            var list = await _infoRequestRepository.GetAll();
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

            return _mapper.Map<List<InfoRequestDto>>(list);
        }

        public async Task<InfoRequestDto> GetByIdAsync(string id)
        {
            var item = await _infoRequestRepository.FindEntityBy(x => x.Id == id);
            return _mapper.Map<InfoRequestDto>(item);
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

                    await SendMailToClient(infoRequestDto.Email);

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
                Console.WriteLine(ex.Message);
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
            catch (System.Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger
            }

            return response;
        }


        private async Task SendMailToClient(string email)
        {
            var emails = new List<KeyValuePair<string, WithEMail>>();
            emails.Add(new KeyValuePair<string, WithEMail>(email, WithEMail.To));
            var subject = "Mensaje de notificación";
            var message = $"<div><p>Hola {email}</p><p>¡Gracias por tu interés en Grupo PerTI!</p><p></p>Nos pondremos en contacto contigo lo más pronto posible.<div>";
            try
            {
                await _emailSenderService.SendEmailAsync(emails, subject, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }
}
