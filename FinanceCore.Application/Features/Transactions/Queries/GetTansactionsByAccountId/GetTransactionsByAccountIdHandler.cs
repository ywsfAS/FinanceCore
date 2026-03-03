using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using MediatR;
using FinanceCore.Domain.Exceptions; 
using System.Threading;

namespace FinanceCore.Application.Features.Transactions.Queries.GetTansactionsByAccountId
{
    public class GetTransactionsByAccountIdHandler : IRequestHandler<GetTransactionsByAccountIdQuery, IEnumerable<TransactionDto>?>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        public GetTransactionsByAccountIdHandler(ITransactionRepository transactionRepository , IAccountRepository accountRepository) { 
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;

        }
        public async Task<IEnumerable<TransactionDto>?> Handle(GetTransactionsByAccountIdQuery query , CancellationToken token
            )
        {
            // Get User Accounts 
            var account = await _accountRepository.GetDtoByIdAndUserIdAsync(query.UserId,query.Id);
            if (account is  null)
            {
                throw new AccountNotFoundException(query.Id);
            }
            return await _transactionRepository.FetchTransactionsByIdPageAsync(query.Id , query.Page , query.PageSize);
           
        }
    }
}
