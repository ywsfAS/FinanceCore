using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.ContactMessage
{
    public sealed record CreateContactMessageRequest(string FullName , string Email , EnMessageSubject Subject , string Message);
}
