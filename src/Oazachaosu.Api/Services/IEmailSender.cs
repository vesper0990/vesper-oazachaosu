using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
