using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Users.Queries.GetFilteredUsers
{
    public sealed record GetUsersQuery(
        string? Search,
        string? Role,
        bool? IsLocked,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PagedResult<UserDto>>;
}
