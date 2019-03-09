using AutoMapper;
using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Events;
using FormsAdmin.Core.Interfaces;
using FormsAdmin.Core.Utilities;
using FormsAdmin.Services.Interfaces;
using System.Collections.Generic;
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

        #region PRIVATE METHODS

        private async Task<PasswordResponse> UpdatePassword(ApplicationUser appUser, string password, bool isAutoPassword, bool requirePasswordChange)
        {
            KeyValuePair<string, string> passwordResult;
            var response = new PasswordResponse();

            if (appUser != null)
            {
                passwordResult = RandomGenerator.EncryptPassword(password, isAutoPassword);
                appUser.PasswordHash = passwordResult.Value;
                await _securityManagerRepository.UpdateUser(appUser);
                await _securityManagerRepository.SaveUser();

                //Response
                response.Success = true;
                response.Email = appUser.UserName;
                response.Id = appUser.Id;
                response.Password = passwordResult.Key;
            }
            else
            {
                response.Message = "No se pudo resetear/cambiar el Password";
            }


            return response;
        }

        #endregion


        #region PUBLIC METHODS

        public async Task<AppUserDto> GetUserWithClaims(AppUser user)
        {
            ApplicationUser appUser = await this._securityManagerRepository.GetUser(user);
            var appUserSingle = _mapper.Map<AppUserDto>(appUser);
            return appUserSingle;
        }

        public async Task<PasswordResponse> Register(RegisterDto appUser)
        {
            var response = new PasswordResponse();
            try
            {
                KeyValuePair<string, string> passwordResult;
               
                var applicationUser = new ApplicationUser();
                applicationUser.Id = Utils.NewGuid;
                applicationUser.UserName = appUser.UserName;
                applicationUser.NormalizedUserName = appUser.UserName.ToUpper();
                applicationUser.Email = appUser.UserName;
                applicationUser.NormalizedEmail = appUser.UserName.ToUpper();
                passwordResult = RandomGenerator.EncryptPassword(appUser.Password, false);
                applicationUser.PasswordHash = passwordResult.Value;

                await _securityManagerRepository.AddUser(applicationUser);
                await _securityManagerRepository.SaveUser();

                //Response
                response.Success = true;
                response.Email = appUser.UserName;
                response.Id = applicationUser.Id;
                response.Password = passwordResult.Key;

               
            }
            catch (System.Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
                //logger 
            }

            return response;
        }

        public async Task<PasswordResponse> ChangePassword(ChangePasswordDto request)
        {
            var passwordEncrypted = Protector.Encrypt(request.CurrentPassword, string.Empty);
            var appUser = await this._securityManagerRepository.GetUserByEmailAndPassword(request.UserName, passwordEncrypted);
            return await UpdatePassword(
               appUser,
               request.NewPassword,
               false,
               false);
        }

        public async Task<PasswordResponse> ResetPassword(ResetPasswordDto request)
        {
            var appUser = await this._securityManagerRepository.GetUserByEmail(request.UserName);
            return await UpdatePassword(
                appUser,
                null,
                true,
                false);
        }
        #endregion
    }
}
