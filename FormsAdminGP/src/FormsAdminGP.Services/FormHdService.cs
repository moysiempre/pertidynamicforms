using AutoMapper;
using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Events;
using FormsAdminGP.Core.Interfaces;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class FormHdService : IFormHdService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IFormHdRepository _formHdRepository;
        private readonly IFormDetailRepository _formDetailRepository;
        private readonly IMapper _mapper;
        public FormHdService(
            IFormHdRepository formHdRepository,
            IFormDetailRepository formDetailRepository,
            IMapper mapper)
        {
            _formHdRepository = formHdRepository;
            _formDetailRepository = formDetailRepository;
            _mapper = mapper;
        }

        #region HEADER  
        public async Task<IEnumerable<FormHdDto>> GetAllAsync()
        {
            var list = await _formHdRepository.FindBy(x=>x.IsActive, t=> t.FormDetails);
            return _mapper.Map<List<FormHdDto>>(list);
        }

        public async Task<FormHdDto> GetByIdAsync(string id)
        {
            var item = await _formHdRepository.FindEntityBy(x => x.Id == id, t => t.FormDetails);
            return _mapper.Map<FormHdDto>(item);
        }

        public async Task<BaseResponse> AddOrUpdateAsync(FormHdDto formHdDto)
        {
            var response = new BaseResponse();
            try
            {               
                var formHd = _mapper.Map<FormHd>(formHdDto);
                if (string.IsNullOrEmpty(formHd.Id))
                {
                    formHd.Id = Core.Utilities.Utils.NewGuid;
                    _formHdRepository.Add(formHd);
                }
                else
                {
                    _formHdRepository.Edit(formHd);
                }

                var item = await _formHdRepository.SaveChanges();

                response.Success = true;
                response.Id = formHd.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
               
            }
            catch (System.Exception ex)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger 
                logger.Error("Error on {0} \n {1} \n {2} \n {3}", ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.Message : string.Empty, ex.InnerException != null ? ex.InnerException.StackTrace : string.Empty);
            }

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
               
                var item = await _formHdRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                    return response;
                }

                _formHdRepository.Delete(item);
                await _formHdRepository.SaveChanges();

                response.Success = true;
                response.Id = id;
                response.Message = LoggingEvents.DELETE_SUCCESS_MESSAGE;
               
            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                //logger 
            }

            return response;
        }
        #endregion

        #region DETAIL 
        public async Task<IEnumerable<FormDetailDto>> GetDetailAllAsync(string formHdId)
        {
            var list = await _formDetailRepository.FindBy(x=>x.FormHdId == formHdId);
            return _mapper.Map<List<FormDetailDto>>(list);
        }

        public async Task<FormDetailDto> GetDetailByIdAsync(string id)
        {
            var item = await _formDetailRepository.FindEntityBy(x => x.Id == id);
            return _mapper.Map<FormDetailDto>(item);
        }

        public async Task<BaseResponse> AddOrUpdateDetailAsync(FormDetailDto formDetailDto)
        {
            var response = new BaseResponse();
            try
            {
                var formDetail = _mapper.Map<FormDetail>(formDetailDto);
                if (string.IsNullOrEmpty(formDetail.Id))
                {
                    formDetail.Id = Core.Utilities.Utils.NewGuid;
                    formDetail.IsActive = true;
                    _formDetailRepository.Add(formDetail);
                }
                else
                {
                    _formDetailRepository.Edit(formDetail);
                }

                var item = await _formDetailRepository.SaveChanges();

                response.Success = true;
                response.Id = formDetail.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;

            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger 
            }

            return response;
        }

        public async Task<BaseResponse> DeleteDetailAsync(string id)
        {
            var response = new BaseResponse();
            try
            {

                var item = await _formDetailRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                    return response;
                }

                _formDetailRepository.Delete(item);
                await _formDetailRepository.SaveChanges();

                response.Success = true;
                response.Id = id;
                response.Message = LoggingEvents.DELETE_SUCCESS_MESSAGE;

            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                //logger 
            }

            return response;
        }
        #endregion



    }
}
