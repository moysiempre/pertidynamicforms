using FormsAdminGP.Common.Enums;
using FormsAdminGP.Services.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FormsAdminGP.Services.EmailSender
{
    public class EmailSenderService : IEmailSenderService
    {
        public EmailSettings _emailSettings;
        public EmailSenderService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public async Task SendEmailAsync(string emailTo, string subject, string message)
        {
            _emailSettings.EmailTo = emailTo;
            _emailSettings.EmailCC = "";
            var emails = SetMails(_emailSettings);
            await Task.Run(() => ExecuteAsync(emails, subject, message, new List<string>()));
        }

        public async Task SendEmailAsync(string emailTo, string subject, string message, List<string> attachments)
        {
            _emailSettings.EmailTo = emailTo;
            var emails = SetMails(_emailSettings);
            await Task.Run(() => ExecuteAsync(emails, subject, message, attachments));
        }

        private async Task<bool> ExecuteAsync(List<KeyValuePair<string, WithEMail>> emails, string subject, string body, List<string> attachments = null)
        {
            bool resp = false;
            try
            {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.EmailFrom, _emailSettings.DisplayName),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body,
                    Priority = MailPriority.High
                };

                //agregar los donde key = true =>(envio normal)
                emails.Where(x => x.Value == WithEMail.To).ToList().ForEach(fe => mail.To.Add(new MailAddress(fe.Key)));

                //agregar los donde key = true =>(con copia oculta)
                emails.Where(x => x.Value == WithEMail.CC).ToList().ForEach(fe => mail.CC.Add(new MailAddress(fe.Key)));

                //agregado de archivo
                if (attachments != null)
                {
                    foreach (string attach in attachments)
                    {
                        //comprobamos si existe el archivo y lo agregamos a los adjuntos
                        if (System.IO.File.Exists(@attach))
                            mail.Attachments.Add(new Attachment(@attach));
                    }
                }


                using (SmtpClient smtp = new SmtpClient(_emailSettings.Smtp))
                {
                    smtp.Port = _emailSettings.Port;
                    smtp.UseDefaultCredentials = true;
                    smtp.EnableSsl = false;
                    smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }


                resp = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resp = false;
            }
            return resp;
        }

        private List<KeyValuePair<string, WithEMail>> SetMails(EmailSettings emailSettings)
        {
            var mails = new List<KeyValuePair<string, WithEMail>>();
            if (!string.IsNullOrEmpty(emailSettings.EmailTo))
            {
                var listEmail = emailSettings.EmailTo.Split(';');
                foreach (var item in listEmail)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mails.Add(new KeyValuePair<string, WithEMail>(item, WithEMail.To));
                    }
                }
            }

            if (!string.IsNullOrEmpty(emailSettings.EmailCC))
            {
                var listEmail = emailSettings.EmailCC.Split(';');
                foreach (var item in listEmail)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mails.Add(new KeyValuePair<string, WithEMail>(item, WithEMail.CC));
                    }
                }
            }
            return mails;
        }

    }
}
