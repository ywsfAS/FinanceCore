
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Alerts
{
    public sealed record CreateLowBalanceAlertCommand(
        Guid AccountId,
        decimal ThresholdAmount) : IRequest<Guid>;
}
