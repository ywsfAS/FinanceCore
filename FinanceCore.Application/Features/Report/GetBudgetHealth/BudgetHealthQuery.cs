using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetBudgetHealth
{
    public record BudgetHealthQuery(Guid UserId , int Page = 1 , int PageSize = 10 ) : IRequest<BudgetHealthDto?>;
}
