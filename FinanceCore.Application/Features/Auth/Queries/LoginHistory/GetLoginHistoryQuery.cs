
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Queries.LoginHistory
{
    public record GetLoginHistoryQuery(Guid UserId , EnLoginStatus? Status , string? Search , DateTime? From , DateTime? To , int Page = 1 , int PageSize = 10) : IRequest<PagedResult<LoginHistoryDto>>;
}
