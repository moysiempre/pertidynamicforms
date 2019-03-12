using AutoMapper;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Core.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class LandindPageService : ILandindPageService
    {
        private readonly ILandingPageRepository _landingPageRepository ;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public LandindPageService(
            ILandingPageRepository landingPageRepository,
            ILogger<LandindPageService> logger,
            IMapper mapper)
        {
            _landingPageRepository = landingPageRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LandingPageDto>> GetAllAsync() {
            _logger.LogInformation($"REQUEST For [GetAllAsync]");
            var list = await _landingPageRepository.GetAll();
            return _mapper.Map<List<LandingPageDto>>(list);
        }

        public async Task<LandingPageDto> GetByIdAsync(string id)
        {
            var item = await _landingPageRepository.FindEntityBy(x=>x.Id == id);
            return _mapper.Map<LandingPageDto>(item);
        }

        public async Task<BaseResponse> AddOrUpdateAsync(LandingPageDto landingPageDto, string userId)
        {
            var response = new BaseResponse();
            try
            {
                var landingPage = _mapper.Map<LandingPage>(landingPageDto);
                if (string.IsNullOrEmpty(landingPage.Id))
                {
                    landingPage.Id = Common.Utilities.Utils.NewGuid; 
                    _landingPageRepository.Add(landingPage);

                }
                else
                {
                    _landingPageRepository.Edit(landingPage);
                }

                var item = await _landingPageRepository.SaveChanges();

                response.Success = true;
                response.Id = landingPage.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger 
            }
 
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(string id, string userId)
        {
            var response = new BaseResponse();
            try
            {                
                var item = await _landingPageRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                    return response;
                }
                _landingPageRepository.Delete(item);
                await _landingPageRepository.SaveChanges();

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
