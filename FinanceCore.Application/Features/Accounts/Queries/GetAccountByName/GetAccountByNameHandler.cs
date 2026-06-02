using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByName
{
    public class GetAccountByNameHandler : IRequestHandler<GetAccountByNameQuery , IEnumerable<AccountDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountByNameHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IEnumerable<AccountDto>?> Handle(GetAccountByNameQuery query , CancellationToken token)
        {
            return await _accountRepository.GetDtoByNameAsync(query.userId , query.name , token);   
        }
    }
}
