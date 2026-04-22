using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSpendingByCategoryPerUser
{
    public sealed record GetSpendingByCategoryPerUserQuery(Guid userId,int year , int month) : IRequest<List<SpendingByCategoryDto>>;
}
