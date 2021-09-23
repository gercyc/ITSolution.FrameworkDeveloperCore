using ITSolution.Framework.Blazor.Application.Requests.Mail;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}