using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Events;
using FormsAdmin.Core.Interfaces;
using FormsAdmin.Core.Responses;
using FormsAdmin.Services.Interfaces;

namespace FormsAdmin.Services
{
    public class InfoRequestService: IInfoRequestService
    {
        private readonly IInfoRequestRepository _infoRequestRepository;
        private readonly IMapper _mapper;
        public InfoRequestService(
            IInfoRequestRepository infoRequestRepository,
            IMapper mapper)
        {
            _infoRequestRepository = infoRequestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InfoRequestDto>> GetAllAsync()
        {
            var list = await _infoRequestRepository.GetAll();
            return _mapper.Map<List<InfoRequestDto>>(list);  
        }

        public async Task<InfoRequestDto> GetByIdAsync(string id)
        {
            var item = await _infoRequestRepository.FindEntityBy(x => x.Id == id);
            return _mapper.Map<InfoRequestDto>(item);
        }

        public async Task<BaseResponse> AddOrUpdateAsync(InfoRequestDto infoRequestDto)
        {
            var response = new BaseResponse();
            try
            {                
                var infoRequest = _mapper.Map<InfoRequest>(infoRequestDto);
                if (string.IsNullOrEmpty(infoRequest.Id))
                {
                    infoRequest.Id = Core.Utilities.Utils.NewGuid;
                    infoRequest.Active = true;
                    _infoRequestRepository.Add(infoRequest);
                }
                else
                {
                    _infoRequestRepository.Edit(infoRequest);
                }

                var item = await _infoRequestRepository.SaveChanges();

                response.Success = true;
                response.Id = infoRequest.Id;
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

        
    }
}
