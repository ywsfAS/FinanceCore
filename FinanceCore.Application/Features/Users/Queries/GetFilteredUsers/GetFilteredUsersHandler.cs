using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Users.Queries.GetFilteredUsers;
using MediatR;

namespace FinanceCore.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _userRepository.GetUsersAsync(
            request.Search,
            request.Role,
            request.IsLocked,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}
