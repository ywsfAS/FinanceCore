using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
    public class GetAccountsOptionsHandler : IRequestHandler<GetAccountsOptionsQuery,IEnumerable<AccountOptionsDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountsOptionsHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IEnumerable<AccountOptionsDto>?> Handle(GetAccountsOptionsQuery query, CancellationToken token)
        {
            return await _accountRepository.GetByUserAccountsOptionsAsync(query.userId,query.page,query.pageSize, token);        }
    }
}
