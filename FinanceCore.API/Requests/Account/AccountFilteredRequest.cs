using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Account
{
    public record AccountFilteredRequest(string? Name , EnCurrency? Currency , EnAccountType? Type, int page = 1 , int pageSize = 10);
}
