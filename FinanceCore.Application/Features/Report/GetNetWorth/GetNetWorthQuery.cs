using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetNetWorth
{
    public record GetNetWorthQuery(Guid UserId) : IRequest<NetWorthDto?>;
    
}
