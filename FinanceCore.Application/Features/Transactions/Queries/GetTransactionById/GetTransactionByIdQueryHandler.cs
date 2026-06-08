using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.DTOs.Transaction;
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
            return await _transactionRepository.GetDtoByIdAndUserId(query.UserId, query.Id, cancellationToken);
        }
    }
}
