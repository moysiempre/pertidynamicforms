using AutoMapper;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public class DDLCatalogService : IDDLCatalogService
    {
        private readonly IDDLCatalogRepository _dDLCatalogRepository;
        private readonly IMapper _mapper;
        public DDLCatalogService(IDDLCatalogRepository dDLCatalogRepository, IMapper mapper)
        {
            _dDLCatalogRepository = dDLCatalogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DDLCatalogDto>> GetAllAsync()
        {
            var listDto = new List<DDLCatalogDto>();
            try
            {
                var list = await _dDLCatalogRepository.GetAll();
                listDto = _mapper.Map<List<DDLCatalogDto>>(list);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return listDto;
        }

        public async Task<DDLCatalogDto> GetByIdAsync(string id)
        {
            DDLCatalogDto itemDto = null;
            try
            {
                var item = await _dDLCatalogRepository.FindEntityBy(x => x.Id == id);
                itemDto = _mapper.Map<DDLCatalogDto>(item);
            }
            catch (Exception ex)
            {
                LoggerService.LogToFile(ex);
            }

            return itemDto;
        }

        public async Task<BaseResponse> AddOrUpdateAsync(DDLCatalogDto catalogDto)
        {
            var response = new BaseResponse();
            try
            {
                var catalog = _mapper.Map<DDLCatalog>(catalogDto);
                if (string.IsNullOrEmpty(catalog.Id))
                {
                    //validar el nombre del landing page
                    var ecatalog = await _dDLCatalogRepository
                        .FindEntityBy(x => x.FormDetailId == catalogDto.FormDetailId &&
                                      x.Name.Trim().ToLower() == catalogDto.Name.Trim().ToLower());
                    if (ecatalog != null)
                    {
                        response.Message = LoggingEvents.INSERT_DUPLICATED_MESSAGE;
                        return response;
                    }

                    catalog.Id = Common.Utilities.Utils.NewGuid;
                    catalog.IsActive = true;
                    _dDLCatalogRepository.Add(catalog);
                    response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
                }
                else
                {
                    _dDLCatalogRepository.Edit(catalog);
                    response.Message = LoggingEvents.UPDATE_SUCCESS_MESSAGE;
                }

                await _dDLCatalogRepository.SaveChanges();
                response.Success = true;
                response.Id = catalog.Id;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                var item = await _dDLCatalogRepository.FindEntityBy(x => x.Id == id);
                if (item == null)
                {
                    response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                    return response;
                }

                _dDLCatalogRepository.Delete(item);
                await _dDLCatalogRepository.SaveChanges();

                response.Success = true;
                response.Id = id;
                response.Message = LoggingEvents.DELETE_SUCCESS_MESSAGE;

            }
            catch (System.Exception ex)
            {
                response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                LoggerService.LogToFile(ex);
            }

            return response;
        }
    }
}
