using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record AccountDto(
        Guid Id,
        Guid UserId,
        string Name,
        EnAccountType Type,
        decimal Balance,
        EnCurrency Currency,
        DateTime CreatedAt);
}
