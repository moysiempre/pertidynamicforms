using FormsAdminGP.Core.EmailSender;
using FormsAdminGP.Core.Enums;
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
        private readonly EmailSettings _emailSettings;
        public EmailSenderService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public async Task SendEmailAsync(List<KeyValuePair<string, WithEMail>> emails, string subject, string message)
        {
            await Task.Run(() => ExecuteAsync(emails, subject, message, new List<string>()));
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
                emails.Where(x => x.Value == WithEMail.Bcc).ToList().ForEach(fe => mail.Bcc.Add(new MailAddress(fe.Key)));

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
                    smtp.EnableSsl = true;
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


    }
}
