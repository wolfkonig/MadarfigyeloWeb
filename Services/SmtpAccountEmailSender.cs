using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MadarfigyeloWeb.Services
{
    public class SmtpAccountEmailSender : IAccountEmailSender
    {
        private readonly SmtpEmailSettings _settings;

        public SmtpAccountEmailSender(IOptions<SmtpEmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendPasswordResetLinkAsync(string email, string resetLink)
        {
            using var message = new MailMessage();
            message.From = string.IsNullOrWhiteSpace(_settings.FromName)
                ? new MailAddress(_settings.FromEmail)
                : new MailAddress(_settings.FromEmail, _settings.FromName);
            message.To.Add(new MailAddress(email));
            message.Subject = "Jelszó-visszaállítás";
            message.IsBodyHtml = true;
            message.Body = $"""
                <html>
                <body>
                    <p>Jelszó-visszaállítási kérelmet kaptunk a fiókodhoz.</p>
                    <p>A jelszó módosításához kattints az alábbi linkre:</p>
                    <p><a href="{resetLink}">Jelszó visszaállítása</a></p>
                    <p>Ha nem te kezdeményezted a kérelmet, ezt az emailt figyelmen kívül hagyhatod.</p>
                </body>
                </html>
                """;

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl
            };

            if (!string.IsNullOrWhiteSpace(_settings.UserName))
            {
                client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
            }
            else
            {
                client.UseDefaultCredentials = true;
            }

            try 
            { 
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email küldése sikertelen: {ex.Message}");
            }
        }
    }
}
