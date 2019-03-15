using AutoMapper;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace FormsAdminGP.RestfulAPI
{
    public class AutoMapperConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LandingPage, LandingPageDto>().ReverseMap();
            CreateMap<FormHd, FormHdDto>().ReverseMap();
            CreateMap<FormDetail, FormDetailDto>().ReverseMap();
            CreateMap<InfoRequest, InfoRequestDto>().ReverseMap();
            CreateMap<DDLCatalog, DDLCatalogDto>().ReverseMap();
            


        }
    }
}
