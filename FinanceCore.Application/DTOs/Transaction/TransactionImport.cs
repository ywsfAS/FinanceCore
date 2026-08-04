
using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record TransactionImport(
     Guid? ToAccountId,
     string? Category,
     decimal Amount,
     EnCurrency Currency,
     EnTransactionType Type,
     DateTime Date,
     string? Description
        );
}
