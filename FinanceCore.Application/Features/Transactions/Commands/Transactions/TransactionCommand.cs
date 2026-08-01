using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.Transactions
{
    public record TransactionCommand(Guid UserId ,Guid AccountId ,Guid? ToAccountId, Guid? CategoryId ,EnTransactionType Type,decimal Amount , string? Description = null , DateTime? TransactionDate = null) : IRequest<TransactionDto>;
}
