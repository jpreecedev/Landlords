namespace Landlords.Services
{
    using System.Threading.Tasks;
    using ViewModels;

    public interface IEmailSender
    {
        Task SendEmailAsync(EmailViewModel email);
    }
}