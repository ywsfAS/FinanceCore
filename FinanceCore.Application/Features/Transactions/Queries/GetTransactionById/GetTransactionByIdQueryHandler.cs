using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(query.Id, cancellationToken);

            if (transaction is null)
                throw new TransactionNotFoundException(query.Id);

            var account = await _accountRepository.GetByIdAsync(transaction.AccountId, cancellationToken);

            if (account is null)
                throw new AccountNotFoundException(transaction.AccountId);

            return new TransactionDto(
                transaction.Id,
                transaction.AccountId,
                transaction.CategoryId,
                transaction.Amount.Amount,
                transaction.Type,
                transaction.Date,
                transaction.Description);
        }
    }
}
