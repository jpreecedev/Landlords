namespace Landlords.Services
{
    using System;
    using System.Threading.Tasks;
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(IOptions<EmailConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task SendEmailAsync(EmailData emailData)
        {
            if (emailData == null) throw new ArgumentNullException(nameof(emailData));

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.Name, _configuration.Address));
            message.To.Add(new MailboxAddress(emailData.RecipientName, emailData.RecipientEmail));
            message.Subject = emailData.Subject;
            
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = emailData.Body;
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