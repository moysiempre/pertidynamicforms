using FormsAdminGP.Common.Enums;
using FormsAdminGP.Common.Events;
using FormsAdminGP.Common.Utilities;
using FormsAdminGP.Data.Repositories.Security;
using FormsAdminGP.Domain;
using FormsAdminGP.Services.EmailSender;
using FormsAdminGP.Services.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public interface IUserService
    {
        Task<string> GetSerialNumberAsync(string userId);
        Task<User> FindUserAsync(string username, string password);
        Task<User> FindUserAsync(string userId);
        Task UpdateUserLastActivityDateAsync(string userId);
        Task<User> GetCurrentUserAsync();
        string GetCurrentUserId();
        Task<(bool Succeeded, string Error)> ChangePasswordAsync(string username, string currentPassword, string newPassword);
        Task<BaseResponse> ResetePasswordAsync(string email);
        Task<BaseResponse> CreateAsync(User user, string roleName);
    }


    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(
            IUserRepository userRepository,
            IEmailSenderService emailSenderService,
            IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _emailSenderService = emailSenderService;
            _contextAccessor = contextAccessor;
        }

        public async Task<User> FindUserAsync(string userId)
        {
            return await _userRepository.FindEntityBy(x => x.Id == userId);
        }

        public async Task<User> FindUserAsync(string username, string password)
        {
            var passwordHash = Utils.GetSha256Hash(password);
            return await _userRepository.FindEntityBy(x => x.Username == username && x.Password == passwordHash);
        }

        public async Task<string> GetSerialNumberAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            return user.SerialNumber;
        }

        public async Task UpdateUserLastActivityDateAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            if (user.LastLoggedIn != null)
            {
                var updateLastActivityDate = TimeSpan.FromMinutes(2);
                var currentUtc = DateTimeOffset.UtcNow;
                var timeElapsed = currentUtc.Subtract(user.LastLoggedIn.Value);
                if (timeElapsed < updateLastActivityDate)
                {
                    return;
                }
            }
            user.LastLoggedIn = DateTimeOffset.UtcNow;

            _userRepository.Edit(user);
            await _userRepository.SaveChanges();

        }

        public string GetCurrentUserId()
        {
            var claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.UserData);
            return userDataClaim?.Value;             
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return await FindUserAsync(userId);
        }

        public async Task<(bool Succeeded, string Error)> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var currentPasswordHash = Utils.GetSha256Hash(currentPassword);
            var user = await FindUserAsync(username, currentPassword);
            if (user.Password != currentPasswordHash)
            {
                return (false, "Current password is wrong.");
            }

            user.Password = Utils.GetSha256Hash(newPassword);
            // user.SerialNumber = Guid.NewGuid().ToString("N"); // To force other logins to expire.

            _userRepository.Edit(user);
            await _userRepository.SaveChanges();
            return (true, string.Empty);
        }

        public async Task<BaseResponse> CreateAsync(User user, string roleName)
        {
            var response = new BaseResponse();
            try
            {
                user.Id = Utils.NewGuid;
                user.IsActive = true;
                user.Password = Utils.GetSha256Hash(user.Password);
                user.SerialNumber = Utils.NewGuid;
                user.DisplayName = user.DisplayName ?? user.Username;

                //user.UserRoles =


                _userRepository.Add(user);
                await _userRepository.SaveChanges();

                response.Success = true;
                response.Id = user.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
            }
            catch (Exception)
            {
                response.Message = LoggingEvents.INSERT_FAILED_MESSAGE;
            }

            return response;
        }


        public async Task<BaseResponse> ResetePasswordAsync(string email)
        {
            var response = new BaseResponse();
            try
            {
                var user = await _userRepository.FindEntityBy(x => x.Username == email);
                if(user == null)
                {
                    response.Message = LoggingEvents.USER_FAILED_MESSAGE;
                    return response;
                }

                var password = Protector.GenerateString(6);
                user.Password = Utils.GetSha256Hash(password);
                _userRepository.Edit(user);
                await _userRepository.SaveChanges();


                //enviar mail
                await SendMailToClient(email, password);

                response.Success = true;
                response.Id = user.Id;
                response.Message = LoggingEvents.INSERT_SUCCESS_MESSAGE;
            }
            catch (Exception)
            {
                response.Message = LoggingEvents.RESETE_FAILED_MESSAGE;
            }

            return response;
        }


        private async Task SendMailToClient(string email, string password)
        {
            var emails = new List<KeyValuePair<string, WithEMail>>();                  
            emails.Add(new KeyValuePair<string, WithEMail>(email, WithEMail.To));
            var subject = "Mensaje de notificación";
            var message = $"<div><p>Hola</p><p>Su contraseña es: <strong>{password}</strong></p><div>";
            try
            {
                await _emailSenderService.SendEmailAsync(email, subject, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
