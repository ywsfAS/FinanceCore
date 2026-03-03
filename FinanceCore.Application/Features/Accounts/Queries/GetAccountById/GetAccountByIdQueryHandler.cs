using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto?> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetDtoByIdAndUserIdAsync(query.UserId,query.Id, cancellationToken);
        }
    }
}
