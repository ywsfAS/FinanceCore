using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSummaryPerUser
{
    public record GetSummaryPerUserQuery(Guid userId) : IRequest<MonthlyUserSummaryDto?>;
}
