using AutoMapper;
using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FormsAdmin.RestfulAPI.Modules
{
    public class AutoMapperModule
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
            CreateMap<ApplicationUser, AppUserDto>().ReverseMap();
            
        }
    }
}
