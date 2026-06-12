using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountFiltered
{
    public class GetAccountFilteredHandler : IRequestHandler<GetAccountFilteredQuery,IEnumerable<AccountInfoDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountFilteredHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IEnumerable<AccountInfoDto>> Handle(GetAccountFilteredQuery query, CancellationToken token)
        {
            return await _accountRepository.GetAccountsAsync(query.UserId, query.Type, query.Currency, query.Name ,query.Page , query.Page ,token);
        }
    }
}
