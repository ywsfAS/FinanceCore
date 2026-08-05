using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Reconcile
{
    public sealed record ReconcileAccountCommand(
        Guid UserId,
        Guid AccountId,
        decimal ActualBalance,
        EnReconciliationReason Reason,
        string? Notes,
        bool CreateAdjustment
    ) : IRequest<ReconciliationDto>;
}
