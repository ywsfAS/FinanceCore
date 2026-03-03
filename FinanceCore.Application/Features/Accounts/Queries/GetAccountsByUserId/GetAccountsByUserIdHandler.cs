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
            return await _accountRepository.GetDtoByUserIdAsync(query.UserId,cancellationToken);
        }
    }
}
