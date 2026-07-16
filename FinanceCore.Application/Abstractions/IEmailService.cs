using FinanceCore.Domain.Common;

namespace FinanceCore.Application.Abstractions
{
    public interface IEmailService
    {
            Task SendEmailAsync(Email email, string subject, string body);
    }
}
