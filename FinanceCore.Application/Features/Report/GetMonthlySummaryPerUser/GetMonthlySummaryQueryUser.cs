using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser
{
    public record GetMonthlySummaryQueryUser(Guid userId , int year , int month) : IRequest<IEnumerable<MonthlySummaryDto>?>;
}
