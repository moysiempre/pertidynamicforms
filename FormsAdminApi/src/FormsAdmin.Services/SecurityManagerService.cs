using AutoMapper;
using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using FormsAdmin.Services.Interfaces;
using System.Threading.Tasks;

namespace FormsAdmin.Services
{
    public class SecurityManagerService : ISecurityManagerService
    {
        private readonly ISecurityManagerRepository _securityManagerRepository;
        private readonly IMapper _mapper;
        public SecurityManagerService(ISecurityManagerRepository securityManagerRepository, IMapper mapper)
        {
            this._securityManagerRepository = securityManagerRepository;
            this._mapper = mapper;
        }
 

        #region PUBLIC METHODS

        public async Task<AppUserDto> GetUserWithClaims(AppUser user)
        {
            ApplicationUser appUser = await this._securityManagerRepository.GetUser(user);
            var appUserSingle = _mapper.Map<AppUserDto>(appUser);
            return appUserSingle;
        }
 
        #endregion
    }
}
