using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(query.Id, cancellationToken);

            if (account is null)
                throw new InvalidOperationException("Account not found.");

            return new AccountDto(
                account.Id,
                account.UserId,
                account.Name,
                account.Type,
                account.Balance.Amount,
                account.Balance.Currency,
                account.CreatedAt);
        }
    }
}
