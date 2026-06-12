using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Account
{

    public record UpdateAccountRequest(string Name,EnAccountType Type);
}
