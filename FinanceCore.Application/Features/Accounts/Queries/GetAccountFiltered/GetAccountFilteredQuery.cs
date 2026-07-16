using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountFiltered
{
    public record  GetAccountFilteredQuery(Guid UserId , string? Name , EnAccountType? Type , EnCurrency? Currency , int Page , int PageSize) : IRequest<IEnumerable<AccountInfoDto>>;
}
