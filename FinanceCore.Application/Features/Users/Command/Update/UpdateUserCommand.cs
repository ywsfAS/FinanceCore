using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Update
{
    public record UpdateUserCommand(
    Guid Id,
    string Name,
    EnCurrency DefaultCurrency,
    string? TimeZone = null) : IRequest;
}
