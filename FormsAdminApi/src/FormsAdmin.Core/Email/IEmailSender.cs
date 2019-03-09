using FormsAdmin.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormsAdmin.Core.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(List<KeyValuePair<string, WithEMail>> emails, string subject, string message);
    }
}
