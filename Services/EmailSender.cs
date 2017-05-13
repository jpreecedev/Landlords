namespace Landlords.Services
{
    using System.Threading.Tasks;
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using ViewModels;

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(IOptions<EmailConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task SendEmailAsync(EmailViewModel email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.Name, _configuration.Address));
            message.To.Add(new MailboxAddress(email.RecipientName, email.RecipientEmail));
            message.Subject = email.Template.Subject;
            
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = email.Template.Body;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync("mail.webcalconnect.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync("admin@webcalconnect.com", "No05K8lGgB");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}