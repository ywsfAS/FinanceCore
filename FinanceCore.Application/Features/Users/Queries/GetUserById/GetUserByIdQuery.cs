using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
}
