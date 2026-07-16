using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Update
{
    public record UpdateUserCommand(
    Guid Id,
    string Name,
    string? TimeZone = null) : IRequest;
}
