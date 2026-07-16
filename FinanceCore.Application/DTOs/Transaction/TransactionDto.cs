using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record TransactionDto(
     Guid Id,
     string AccountName,
     string? ToAccountName,
     string? CategoryName,
     decimal Amount,
     EnCurrency Currency,
     EnTransactionType Type,
     DateTime Date,
     string? Description);
}
