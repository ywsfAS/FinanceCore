using FinanceCore.Application.DTOs.RecurringTransaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.queries.GetRecurringById
{
    public record GetRecurringByIdQuery(Guid UserId , Guid Id) : IRequest<RecurringTransactionDto?>;
}
