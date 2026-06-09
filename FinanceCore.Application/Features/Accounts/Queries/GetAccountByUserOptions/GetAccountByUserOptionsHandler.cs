using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountByName;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
    public class GetAccountByUserOptionsHandler : IRequestHandler<GetAccountByUserOptionsQuery,IEnumerable<AccountOptionsDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountByUserOptionsHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IEnumerable<AccountOptionsDto>?> Handle(GetAccountByUserOptionsQuery query, CancellationToken token)
        {
            return await _accountRepository.GetByUserAccountsOptionsAsync(query.userId, token);        }
    }
}
