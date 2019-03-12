using FormsAdminGP.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.EmailSender
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(List<KeyValuePair<string, WithEMail>> emails, string subject, string message);
    }
}
