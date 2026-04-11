using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummary
{
    public record GetMonthlySummaryQuery(Guid UserId ,Guid Id, int year , int month) : IRequest<MonthlySummaryDto?>;
    
   
}
