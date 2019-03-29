using AutoMapper;
using FormsAdminGP.Common.Enums;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Core.Interfaces;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using Microsoft.Extensions.Options;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class FormHdService : IFormHdService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IFormHdRepository _formHdRepository;
        private readonly IFormDetailRepository _formDetailRepository;
        private readonly ILandingPageRepository _landingPageRepository;
        private readonly IDDLCatalogRepository _dDLCatalogRepository;
        private readonly IOptions<List<BaseDetailSettings>> _baseDetailSettings;
        private readonly IMapper _mapper;
        public FormHdService(
            IFormHdRepository formHdRepository,
            IFormDetailRepository formDetailRepository,
            ILandingPageRepository landingPageRepository,
            IDDLCatalogRepository dDLCatalogRepository,
            IOptions<List<BaseDetailSettings>> baseDetailSettings,
            IMapper mapper)
        {
            _formHdRepository = formHdRepository;
            _formDetailRepository = formDetailRepository;
            _landingPageRepository = landingPageRepository;
            _dDLCatalogRepository = dDLCatalogRepository;
            _baseDetailSettings = baseDetailSettings;
            _mapper = mapper;
        }

        #region HEADER  
        public async Task<IEnumerable<FormHdDto>> GetAllAsync()
        {
            var list = await _formHdRepository.FindBy(x=>x.IsActive, t=> t.FormDetails);
            foreach (var item in list)
            {
                item.FormDetails = item.FormDetails.OrderBy(x => x.Order).ToList();
                var formHdLandingPage = await _landingPageRepository.FindBy(x => x.FormHdId == item.Id);
                item.LandingPages = formHdLandingPage.ToList();
                foreach (var detail in item.FormDetails.Where(x=>x.FieldTypeId == FieldType.select.ToString()))
                {
                    var ddlCatalogs = await _dDLCatalogRepository.FindBy(x => x.FormDetailId == detail.Id);
                    detail.DDLCatalogs = ddlCatalogs.ToList();
                }
            }

            var listDto = _mapper.Map<List<FormHdDto>>(list);          
            return listDto;
        }

        public async Task<FormHdDto> GetByIdAsync(string id)
        {
            var item = await _formHdRepository.FindEntityBy(x => x.Id == id, t => t.FormDetails);
            if(item != null)
            {
                item.FormDetails = item.FormDetails.Where(x=>x.IsActive).OrderBy(x => x.Order).ToList();
                foreach (var detail in item.FormDetails.Where(x => x.FieldTypeId == FieldType.select.ToString()))
                {
                    var ddlCatalogs = await _dDLCatalogRepository.FindBy(x => x.FormDetailId == detail.Id);
                    detail.DDLCatalogs = ddlCatalogs.ToList();
                }
            }
           
            return _mapper.Map<FormHdDto>(item);
        }

        public async Task<FormHdDto> GetByLandingPageIdAsync(string landingPageId)
        {
            var landings = await _landingPageRepository.FindBy(x => x.Id == landingPageId && x.IsActive);
            if (landings.Count() == 0)
            {
                return new FormHdDto();
            }

            var formHdId = landings.FirstOrDefault().FormHdId;
            var item = await _formHdRepository.FindEntityBy(x => x.Id == formHdId, t => t.FormDetails);
            if (item != null)
            {
                item.FormDetails = item.FormDetails.Where(x => x.IsActive).OrderBy(x => x.Order).ToList();
                foreach (var detail in item.FormDetails.Where(x => x.FieldTypeId == FieldType.select.ToString()))
                {
                    var ddlCatalogs = await _dDLCatalogRepository.FindBy(x => x.FormDetailId == detail.Id);
                    detail.DDLCatalogs = ddlCatalogs.ToList();
                }
            }

            return _mapper.Map<FormHdDto>(item);             
        }

        public async Task<IEnumerable<FormHdDto>>  GetAllForOptionsAsync()
        {
            var list = await _formHdRepository.GetAll();
            return _mapper.Map<List<FormHdDto>>(list);            
        }
        public async Task<BaseResponse> AddOrUpdateAsync(FormHdDto formHdDto)
        {
            var response = new BaseResponse();
            try
            {               
                var formHd = _mapper.Map<FormHd>(formHdDto);
                if (string.IsNullOrEmpty(formHd.Id))
                {
                    //validar el nombre del landing page
                    var form = await _formHdRepository.FindEntityBy(x => x.Name.Trim().ToLower() == formHdDto.Name.Trim().ToLower());
                    if (form != null)
                    {
                        response.Message = LoggingEvents.INSERT_DUPLICATED_MESSAGE;
                        return response;
                    }

                    formHd.Id = Common.Utilities.Utils.NewGuid;
                    _formHdRepository.Add(formHd);


                    foreach (var page in formHdDto.LandingPages)
                    {
                        var landing = await _landingPageRepository.FindEntityBy(x => x.Id == page.Id);
                        if (landing != null)
                        {
                            if (landing.FormHdId != null)
                            {
                                response.Message = $"El landing page: { landing.Name } ya cuenta con un formulario asignado, es recomendable darle de baja antes";
                            }
                            else
                            {
                                landing.FormHdId = formHd.Id;
                                _landingPageRepository.Edit(landing);
                            }
                        }
                    } 


                    foreach (var detail in formHd.FormDetails)
                    {
                        if(detail.DDLCatalogs != null && detail.DDLCatalogs.Count > 0)
                        {
                            foreach (var cat in detail.DDLCatalogs)
                            {
                                cat.IsActive = true;
                                cat.FormDetailId = detail.Id;                               
                                _dDLCatalogRepository.Add(cat);
                            }
                        }                       
                    }

                }
                else
                {

                    foreach (var page in formHdDto.LandingPages)
                    {
                        var landing = await _landingPageRepository.FindEntityBy(x => x.Id == page.Id);
                        if (landing != null)
                        {
                            if (landing.FormHdId != null)
                            {
                                response.Message = $"El landing page: { landing.Name } ya cuenta con un formulario asignado, es recomendable darle de baja antes";
                            }
                            else
                            {
                                landing.FormHdId = formHd.Id;
                                _landingPageRepository.Edit(landing);
                            }
                        }
                    }


                    foreach (var detalleDto in formHd.FormDetails)
                    {
                        _formDetailRepository.Edit(detalleDto);
                    }


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

         

        public async Task<BaseResponse> UpdateFormHdFileAsync(string id, string fileName)
        {
            var response = new BaseResponse();
            try
            {

                var item = await _formHdRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.UPDATE_FAILED_MESSAGE;
                    return response;
                }

                item.FilePath = fileName;
                _formHdRepository.Edit(item);
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

        public async Task<IEnumerable<BaseDetailSettings>> GetBaseDetail()
        {
            var list = new List<BaseDetailSettings>();
            await Task.Run(() =>
            {
                //list = Enum.GetValues(typeof(FieldType)).Cast<object>()
                //.Select(p => new FormDetailDto { FormHdId = Convert.ToInt32(p).ToString(),  FieldLabel = ((Enum)p).GetEnumDescription(), FieldTypeId = ((Enum)p).ToString() })
                //.OrderBy(p => p.FormHdId).ToList();
                list = _baseDetailSettings.Value.ToList();
            });
            return list;
        }

        public async Task<BaseResponse> AddOrUpdateDetailAsync(FormDetailDto formDetailDto)
        {
            var response = new BaseResponse();
            try
            {
                var formDetail = _mapper.Map<FormDetail>(formDetailDto);
                if (string.IsNullOrEmpty(formDetail.Id))
                {
                    formDetail.Id = Common.Utilities.Utils.NewGuid;
                    formDetail.IsActive = true;
                    _formDetailRepository.Add(formDetail);
                }
                else
                {
                    formDetail = await _formDetailRepository.FindEntityBy(x => x.Id == formDetailDto.Id);
                    if (formDetail != null)
                    {
                        formDetail.FieldLabel = formDetailDto.FieldLabel;
                        formDetail.Order = formDetailDto.Order;
                        _formDetailRepository.Edit(formDetail);
                    }
                    else {

                        response.Message = LoggingEvents.UPDATE_FAILED_MESSAGE;
                        return response;
                    }

                   
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
                    response.Message = LoggingEvents.UPDATE_FAILED_MESSAGE;
                    return response;
                }

                item.IsActive = false;
                _formDetailRepository.Edit(item);
                await _formDetailRepository.SaveChanges();

                response.Success = true;
                response.Id = id;
                response.Message = LoggingEvents.UPDATE_SUCCESS_MESSAGE;

            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.UPDATE_FAILED_MESSAGE;
                //logger 
            }

            return response;
        }
        #endregion



    }
}
