using FinanceCore.Application.DTOs.Transaction;
using MediatR;


namespace FinanceCore.Application.Features.Transactions.Queries.GetTransactionById
{
    public record GetTransactionByIdQuery(Guid UserId ,Guid Id) : IRequest<TransactionDto?>;
}
