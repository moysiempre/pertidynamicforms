using FormsAdminGP.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.EmailSender
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string emailTo, string subject, string message);
        Task SendEmailAsync(string emailTo, string subject, string message, List<string> attachments);
    }
}
