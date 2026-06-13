using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetMonthlySummary
{
    public record GetAccountsMonthlySummaryQuery(Guid UserId ,Guid? AccountId, int Year , int Month , int Page , int PageSize) : IRequest<IEnumerable<MonthlySummaryDto>>;
    
   
}
