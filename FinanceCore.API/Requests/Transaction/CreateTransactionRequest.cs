using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Transaction
{
    public record CreateTransactionRequest(Guid AccountId ,Guid CategoryId ,EnTransactionType Type,decimal Amount , EnCurrency Currency, string? Description, DateTime TransactionDate);
    
}
