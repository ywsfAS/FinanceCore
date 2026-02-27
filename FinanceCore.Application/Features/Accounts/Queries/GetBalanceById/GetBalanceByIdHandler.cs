using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetBalanceById
{
    public class GetBalanceByIdHandler : IRequestHandler<GetBalanceByIdQuery,AccountBalanceDto>
    {
        private readonly IAccountRepository _accountRepository;
        public GetBalanceByIdHandler(IAccountRepository accountRepository) { 
              _accountRepository = accountRepository;
        }
        public async Task<AccountBalanceDto> Handle(GetBalanceByIdQuery Query , CancellationToken token)
        {
            var account = await _accountRepository.GetByIdAsync(Query.AccountId, token);

            if (account is null)
                throw new InvalidOperationException("Account not found.");

            return new AccountBalanceDto(account.Id, account.Name,account.Balance);



        }
    }
}
