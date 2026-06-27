using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.ContributionsTrend
{
    public record ContributionsTrendQuery(Guid UserId , int LastNMonth) : IRequest<ContributionsTrendDto?>;
}
