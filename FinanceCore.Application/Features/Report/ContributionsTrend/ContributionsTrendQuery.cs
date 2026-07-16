using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.ContributionsTrend
{
    public record ContributionsTrendQuery(Guid UserId , int LastNMonth) : IRequest<ContributionsTrendDto?>;
}
