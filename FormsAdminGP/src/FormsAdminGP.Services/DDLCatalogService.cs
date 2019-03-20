using AutoMapper;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
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
            var list = await _dDLCatalogRepository.GetAll();
            var listDto = _mapper.Map<List<DDLCatalogDto>>(list);
            return listDto;
        }

        public async Task<DDLCatalogDto> GetByIdAsync(string id)
        {
            var item = await _dDLCatalogRepository.FindEntityBy(x => x.Id == id);           
            return _mapper.Map<DDLCatalogDto>(item);
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
                }
                else
                {
                    _dDLCatalogRepository.Edit(catalog);
                }

                await _dDLCatalogRepository.SaveChanges();
                response.Success = true;
                response.Id = catalog.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
            }
            catch (Exception ex)
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
            catch (System.Exception)
            {
                response.Message = LoggingEvents.DELETE_FAILED_MESSAGE;
                //logger 
            }

            return response;
        }
    }
}
