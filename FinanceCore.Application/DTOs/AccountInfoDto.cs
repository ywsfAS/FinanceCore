using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record AccountInfoDto(Guid Id , string Name , EnAccountType Type , decimal Balance , EnCurrency Currency);
}
