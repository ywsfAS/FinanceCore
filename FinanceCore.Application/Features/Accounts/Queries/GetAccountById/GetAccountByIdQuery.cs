using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountById
{
    public record GetAccountByIdQuery(Guid UserId,Guid Id) : IRequest<AccountDto?>;
}
