
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Delete
{
    
    public record DeleteUserCommand(Guid Id) : IRequest;

}
