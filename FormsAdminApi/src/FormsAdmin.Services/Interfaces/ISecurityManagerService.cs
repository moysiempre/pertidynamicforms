using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsAdmin.Services.Interfaces
{
    public interface ISecurityManagerService
    {
        Task<AppUserDto> GetUserWithClaims(AppUser user);
        //Task<PasswordResponse> ChangePassword(ChangePasswordViewModel request);
        //Task<PasswordResponse> ResetPassword(ResetPasswordViewModel request);
    }
}
