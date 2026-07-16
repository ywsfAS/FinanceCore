using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Delete
{
    public record DeleteAccountCommand(Guid UserId , Guid Id) : IRequest;
}
