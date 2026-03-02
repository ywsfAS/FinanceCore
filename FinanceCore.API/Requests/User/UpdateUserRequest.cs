using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.User
{
    public record UpdateUserRequest (
        string Name,
        EnCurrency DefaultCurrency,
        string? TimeZone = null);
}
