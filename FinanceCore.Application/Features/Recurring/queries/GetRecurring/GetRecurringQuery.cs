using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.queries.GetRecurring
{
    public record GetRecurringQuery(Guid UserId , Guid? AccountId, Guid? CategoryId ,bool? IsActive , EnPeriod? Period ,DateTime? Start , DateTime? End , int Page = 1 , int PageSize = 10) : IRequest<IEnumerable<RecurringTransactionDto>>;
}
