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
        public async Task<AccountBalanceDto> Handle(GetBalanceByIdQuery query , CancellationToken token)
        {
            var balance = await _accountRepository.GetTotalBalanceAsync(query.UserId, token);
            return new AccountBalanceDto(query.UserId,balance);

        }
    }
}
