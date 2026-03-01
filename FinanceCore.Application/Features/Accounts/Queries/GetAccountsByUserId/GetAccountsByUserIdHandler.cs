using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountsByUserId
{
    public class GetAccountsByUserIdHandler : IRequestHandler<GetAccountsByUserIdQuery,IEnumerable<AccountDto>?>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountsByUserIdHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountDto>?> Handle(GetAccountsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetByUserIdAsync(query.UserId, cancellationToken);
            if (accounts is null)
                throw new InvalidOperationException("Account not found.");

            return accounts.Select(account => new AccountDto(
                account.Id,
                account.UserId,
                account.Name,
                account.Type,
                account.Balance.Amount,
                account.Currency,
                account.CreatedAt));
        }
    }
}
