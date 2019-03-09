using FormsAdmin.Core;
using FormsAdmin.Core.DTO;
using System.Threading.Tasks;

namespace FormsAdmin.Services.Interfaces
{
    public interface ISecurityManagerService
    {
        Task<AppUserDto> GetUserWithClaims(AppUser user);
        Task<PasswordResponse> Register(RegisterDto appUser);
        Task<PasswordResponse> ChangePassword(ChangePasswordDto request);
        Task<PasswordResponse> ResetPassword(ResetPasswordDto request);
    }
}
