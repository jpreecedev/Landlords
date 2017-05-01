namespace Landlords.Services
{
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(EmailData emailData);
    }
}