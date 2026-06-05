using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Transaction
{
    public record CreateTransferRequest(Guid AccountId, Guid ToAccountId,decimal Amount , EnCurrency Currency ,string? Description, string? notes);
}
