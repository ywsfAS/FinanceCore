using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.AssignRole
{
    public record AssignRoleCommand(Guid UserId , UserRole Role) : IRequest;
}
