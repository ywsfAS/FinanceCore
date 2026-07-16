using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
    public record GetAccountsOptionsQuery(Guid userId , int page = 1 , int pageSize = 10) : IRequest<IEnumerable<AccountOptionsDto>>;
}
