using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlyTrend
{
    public record MonthlyTrendQuery(Guid userId , int month) : IRequest<IEnumerable<MonthlyTrendDto>?>;
}
