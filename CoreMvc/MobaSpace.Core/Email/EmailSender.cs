using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MobaSpace.Core.Email
{
    public class EmailSender
    {
        private readonly EmailSettings _settings;
        private readonly List<EmailResource> _resources = new List<EmailResource>();

        public EmailSender(IOptions<EmailSettings> emailsettings, IFileProvider fileProvider)
        {
            _settings = emailsettings.Value;
            _resources.Add(new EmailResource("logo", fileProvider.GetFileInfo("./images/mobaspace.png")));
        }

        public async Task SendEmailAsync(string email, string subject, string[] messages, EmailResource[] resources = null)
        {
            using (var client = new SmtpClient())
            {
                if (!string.IsNullOrEmpty(_settings.UserName))
                {
                    client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
                }
                client.Host = _settings.Host;
                client.Port = _settings.Port;
                client.EnableSsl = _settings.UseSsl;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_settings.SenderEmail);
                    emailMessage.Subject = subject;
                    emailMessage.Body = messages[0];
                    emailMessage.IsBodyHtml = false;

                    for (int i = 1; i < messages.Length; i++)
                    {
                        AlternateView view = null;
                        switch (i)
                        {
                            case 0:
                                break;

                            case 1:
                                view = AlternateView.CreateAlternateViewFromString(messages[i], new ContentType(MediaTypeNames.Text.Html));
                                if (resources is null) continue;
                                foreach (var resource in resources)
                                {
                                    view.LinkedResources.Add(new LinkedResource(new MemoryStream(resource.Content), new ContentType("image/png"))
                                    {
                                        ContentId = resource.ContentId
                                    });
                                }
                              

                                break;
                            default:
                                break;
                        }
                        if (view != null)
                        {
                            emailMessage.AlternateViews.Add(view);
                        }
                    }

                    await client.SendMailAsync(emailMessage);
                }
            }
        }
    }
}
