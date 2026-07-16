using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetMonthlyTrend
{
    public record MonthlyTrendQuery(Guid userId , int month) : IRequest<IEnumerable<MonthlyTrendDto>?>;
}
