using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Transaction
{
    public record CreateTransactionRequest(Guid AccountId, Guid? ToAccountId ,Guid? CategoryId ,EnTransactionType Type,decimal Amount , string? Description, DateTime TransactionDate);
    
}
