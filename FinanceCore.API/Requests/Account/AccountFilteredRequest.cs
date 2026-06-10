using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Account
{
    public record AccountFilteredRequest(string? name , EnCurrency? currency , EnAccountType? type);
}
