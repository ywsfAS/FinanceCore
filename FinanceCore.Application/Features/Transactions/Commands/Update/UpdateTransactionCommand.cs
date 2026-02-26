using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Update
{
    public record UpdateTransactionCommand(
        Guid Id,
        Guid? CategoryId,
        decimal Amount,
        DateTime Date,
        string? Description = null) : IRequest;
}
