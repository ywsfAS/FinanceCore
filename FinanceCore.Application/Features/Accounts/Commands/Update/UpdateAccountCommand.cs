using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Update
{
    public record UpdateAccountCommand(
        Guid UserId,
        Guid AccountId,
        string Name,
        EnAccountType AccountType
        ) : IRequest;

}
