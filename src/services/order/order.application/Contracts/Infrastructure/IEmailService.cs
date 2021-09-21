using order.application.Models;
using System.Threading.Tasks;

namespace order.application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendMail(Email email);
    }
}
