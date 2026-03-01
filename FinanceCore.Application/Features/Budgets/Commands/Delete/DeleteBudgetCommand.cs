using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Commands.Delete
{
    public record DeleteBudgetCommand(Guid UserId ,Guid Id) : IRequest;
}
