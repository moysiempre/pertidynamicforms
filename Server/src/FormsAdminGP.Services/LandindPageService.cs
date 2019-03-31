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
        private readonly IMapper _mapper;
        public LandindPageService(
            ILandingPageRepository landingPageRepository,
            ILogger<LandindPageService> logger,
            IMapper mapper)
        {
            _landingPageRepository = landingPageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LandingPageDto>> GetAllAsync()
        {
            var listDto = new List<LandingPageDto>();
            try
            {
                var list = await _landingPageRepository.GetAll();
                listDto = _mapper.Map<List<LandingPageDto>>(list);
            }
            catch (System.Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return listDto;
        }

        public async Task<IEnumerable<LandingPageDto>> GetAllOptionsAsync()
        {
            var listDto = new List<LandingPageDto>();
            try
            {
                var list = await _landingPageRepository.GetAll(x=>x.FormHdId == null);
                listDto = _mapper.Map<List<LandingPageDto>>(list);
            }
            catch (System.Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return listDto;
        }

        public async Task<LandingPageDto> GetByIdAsync(string id)
        {
            LandingPageDto itemDto = null;
            try
            {
                var item = await _landingPageRepository.FindEntityBy(x => x.Id == id);
                itemDto = _mapper.Map<LandingPageDto>(item);
            }
            catch (System.Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return itemDto;
        }

        public async Task<BaseResponse> AddOrUpdateAsync(LandingPageDto landingPageDto, string userId)
        {
            var response = new BaseResponse();
            try
            {               

                //mappear el DTO
                var landingPage = _mapper.Map<LandingPage>(landingPageDto);
                if (string.IsNullOrEmpty(landingPage.Id))
                {

                    //validar el nombre del landing page
                    var landing = await _landingPageRepository.FindEntityBy(x => x.Name.Trim().ToLower() == landingPageDto.Name.Trim().ToLower());
                    if (landing != null)
                    {
                        response.Message = LoggingEvents.INSERT_DUPLICATED_MESSAGE;
                        return response;
                    }


                    landingPage.Id = Common.Utilities.Utils.NewGuid; 
                    _landingPageRepository.Add(landingPage);

                }
                else
                {
                    landingPage.FormHdId = (string.IsNullOrEmpty(landingPage.FormHdId)) ? null : landingPage.FormHdId;
                    _landingPageRepository.Edit(landingPage);
                }

                var item = await _landingPageRepository.SaveChanges();

                response.Success = true;
                response.Id = landingPage.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
            }
            catch (System.Exception ex)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                LoggerService.LogToFile(ex);
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
            catch (System.Exception ex)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                LoggerService.LogToFile(ex);
            }

            return response;
        }
    }
}
