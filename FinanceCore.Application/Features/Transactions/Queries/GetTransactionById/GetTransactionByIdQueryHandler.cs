using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Application.DTOs.Transaction;
namespace FinanceCore.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto?> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            return await _transactionRepository.GetDtoByIdAndUserId(query.UserId, query.Id, cancellationToken);
        }
    }
}
