using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Queries.GetFiltredTransactions
{
    public record GetFiltredTransactionsQuery(Guid UserId , Guid? AccountId , Guid?ToAccountId , Guid? CategoryId , DateTime? Start , DateTime? End , EnTransactionType? Type , int Page , int PageSize) : IRequest<IEnumerable<TransactionDto>>;
}
