using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSpendingByCategory
{
    public record GetSpendingByCategoryQuery(Guid UserId , Guid? AccountId , int year , int month ) : IRequest<IEnumerable<SpendingByCategoryDto>>;
}
