using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Account
{
    public record CreateAccountRequest(string Name , EnAccountType Type , EnCurrency Currency , decimal InitialBalance = 0);
}
