using FinanceCore.Application.DTOs.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Queries.GetRecurringById
{
    public record GetRecurringByIdQuery(Guid UserId , Guid Id) : IRequest<RecurringTransactionDto?>;
}
