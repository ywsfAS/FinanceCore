using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

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
