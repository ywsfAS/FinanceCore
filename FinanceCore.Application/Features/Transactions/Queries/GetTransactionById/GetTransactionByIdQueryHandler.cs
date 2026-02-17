using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(query.Id, cancellationToken);

            if (transaction is null)
                throw new TransactionNotFoundException(query.Id);

            return new TransactionDto(
                transaction.Id,
                transaction.AccountId,
                transaction.CategoryId,
                transaction.Amount.Amount,
                transaction.Amount.Currency,
                transaction.Type,
                transaction.Date,
                transaction.Description);
        }
    }
}
