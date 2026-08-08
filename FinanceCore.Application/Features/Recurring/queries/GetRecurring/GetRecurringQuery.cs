using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Queries.GetRecurring
{
    public record GetRecurringQuery(Guid UserId , Guid? AccountId, Guid? CategoryId ,EnRecurringTransactionStatus? Status , EnPeriod? Period ,DateTime? Start , DateTime? End , int Page = 1 , int PageSize = 10) : IRequest<IEnumerable<RecurringTransactionDto>>;
}
