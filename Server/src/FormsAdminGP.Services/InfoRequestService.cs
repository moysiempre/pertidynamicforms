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
using System.Text;

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
                //DateTime startDate = DateTime.MinValue ;
                //if (request.StartDate.HasValue)
                //{
                //    DateTime.TryParse(request.StartDate.Value.ToString("yyyy-MM-dd"), out startDate);
                //}
                //DateTime endDate = DateTime.MinValue;
                //if (request.EndDate.HasValue)
                //{
                //    DateTime.TryParse(request.StartDate.Value.ToString("yyyy-MM-dd"), out endDate);
                //}

                var list = await _infoRequestRepository.FindBy(x => x.IsActive, x => x.LandingPage);
                if (!string.IsNullOrEmpty(request.LandingPageId))
                {
                    list = list.Where(x => x.LandingPageId == request.LandingPageId);
                }

                if (request.StartDate.HasValue  && request.EndDate.HasValue)
                {
                    var endDate = request.EndDate.Value.Date.AddDays(1);
                    list = list.Where(x => x.RequestDate.Date >= request.StartDate.Value.Date && x.RequestDate <= endDate.Date);
                }
                else
                {
                    if (request.StartDate.HasValue)
                    {
                        list = list.Where(x => x.RequestShortDate.Date >= request.StartDate.Value.Date);
                    }
                    else if (request.EndDate.HasValue)
                    {
                        list = list.Where(x => x.RequestDate.Date <= request.EndDate.Value.Date);
                    }
                    else
                    {
                        var end = DateTime.Now;
                        var start = end.AddDays(-15);
                        list = list.Where(x => x.RequestDate.Date >= start.Date && x.RequestDate.Date <= end.Date);
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
                        attach = Path.Combine(baseDir, infoRequestDto.FormHdId, infoRequestDto.FileName);                       
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

        private async Task SendMailToClient(string emailTo, string name, string attach, string mailTemplateId)
        {
             
            var attachs = new List<string>();
            if(!string.IsNullOrEmpty(attach))
            {
                attachs.Add(attach);
            };
            name = name ?? emailTo;             

           var mailTemplate = await _mailTemplateService.GetByIdAsync(mailTemplateId);
            var subject = "Mensaje de notificación";
            var message = $"<div><p>Hola {name}</p><p>¡Gracias por tu interés en Grupo PerTI!</p><p></p>Nos pondremos en contacto contigo lo más pronto posible.<div>";
            if(mailTemplate != null)
            {
                subject = mailTemplate.Subject;
                message = SetTemplate(mailTemplate, name, true);
            }

            try
            {
                await _emailSenderService.SendEmailAsync(emailTo, subject, message, attachs);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }
        }

        

        string SetTemplate(MailTemplateDto mailTemplate, string name, bool flag)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<div style='text-align:center; background-color:#f7f7f7 ; padding: 15px 0'> <img src='https://www.grupoperti.com.mx/img/logo.svg' alt='logo'/></div>");
            //var saludos = $"<p>{mailTemplate.Salut} {name}</p>";         
            //sb.Append($"{saludos} {mailTemplate.Body}");
            //sb.Append(@" <div class='footer'> <div class='box'> <div class='r-sociales'> <a href='https://www.facebook.com/GrupoPerTI/'> <img src='https://www.grupoperti.com.mx/img/facebook.png' alt=''/> </a> <a href='' style='display:none'><img class='' src='/images/facebook.png' alt=''> </a> <a href='https://www.linkedin.com/company/grupo-perti/?viewAsMember=true'> <img src='https://www.grupoperti.com.mx/img/linkedin.png' alt=''/> </a> </div><hr style='border: 1px solid #595959; margin: 70px 0'> <p class='mb-0 mt-0'>Copyright © *|2019|* *|Grupo PerTI|*, All rights reserved.</p><p class='mb-0 mt-0'><strong>Our mailing address is:</strong></p><a href='mailto:ana.caballero@grupoperti.com.mx'>ana.caballero@grupoperti.com.mx</a> <p class='mb-0'> Want to change how you receive these emails?</p><p class='mt-0'>You can <a class='no-link' href='https://grupoperti.us16.list-manage.com/profile?u=f2e92be4dbcd75bdecef25f47&id=eccf867244&e='>update your preferences</a> or <a class='no-link' href='https://grupoperti.us16.list-manage.com/unsubscribe?u=f2e92be4dbcd75bdecef25f47&id=eccf867244&e=&c=f3c4a11368'>unsubscribe from this list.</a> </p></div></div><style>.footer{padding-top: 20px; text-align: center; background-color: #333333; color: #fff; padding-bottom: 50px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px;}.footer>.box{width: 50%; margin: 0 auto}.footer .r-sociales{padding-top: 50px;}.footer .r-sociales img{width: 30px; margin: 0 10px;}.footer .r-sociales img.bordered{width: 30px; border: 1px solid #fff; border-radius: 50%;}.mt-0{margin-top: 0}.mb-0{margin-bottom: 0}.no-link, .no-link:focus, .no-link:link{color: #fff;}.no-link:hover{color: #fff; text-decoration: none}</style>");
            //return sb.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/> </head> <body>");
            sb.Append("<div style='text-align:center; background-color:#f7f7f7 ; padding: 15px 0'> <img src='https://www.grupoperti.com.mx/img/logo.svg' alt='logo'/></div>");
            sb.Append("<br/>");
            sb.Append($"<p>{mailTemplate.Salut} {name}</p>");
            sb.Append($"{mailTemplate.Body}");
            sb.Append("<br/><br/>");
            sb.Append("<table width='100%' bgcolor='#333333' style='color:#fff; font-family: sans-serif;font-size: 12px;' > <tr> <td align='center'> <br/> <br/> <br/> <a href='https://www.facebook.com/GrupoPerTI/'> <img src='https://www.grupoperti.com.mx/img/facebook.png' alt='facebook' style='width: 30px;margin: 0 5px;' width='30'/> </a> <a href='' style='display:none' ><img class='' src='/images/facebook.png' alt='facebook' style='width: 30px;margin: 0 5px;' width='30px'/> </a> <a href='https://www.linkedin.com/company/grupo-perti/?viewAsMember=true' > <img src='https://www.grupoperti.com.mx/img/linkedin.png' alt='linkedin' style='width: 30px;margin: 0 5px;' width='30'/> </a> </td></tr><tr> <td align='center'> <hr width='50%' style='border: 1px solid #595959; margin: 40px 0'/> </td></tr><tr> <td align='center'> <p style='margin-top: 0;margin-bottom: 0'> Copyright © *|2019|* *|Grupo PerTI|*, All rights reserved. </p><p style='margin-top: 0;margin-bottom: 0'> <strong>Our mailing address is:</strong> </p><a href='mailto:ana.caballero@grupoperti.com.mx' >ana.caballero@grupoperti.com.mx</a > <p style='margin-bottom: 0'> Want to change how you receive these emails? </p><p style='margin-top: 0; margin-bottom: 50px'> You can <a style='color: #fff;' href='https://grupoperti.us16.list-manage.com/profile?u=f2e92be4dbcd75bdecef25f47&id=eccf867244&e=' >update your preferences</a > or <a style='color: #fff;' href='https://grupoperti.us16.list-manage.com/unsubscribe?u=f2e92be4dbcd75bdecef25f47&id=eccf867244&e=&c=f3c4a11368' >unsubscribe from this list.</a > </p><br/> <br/> </td></tr></table>");
            sb.Append("</body></html>");
            return sb.ToString();
        }
         
    }
}
