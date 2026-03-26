using MediatR;
using System;

namespace FinanceCore.Application.Features.RecurringTransaction.Commands.Delete
{
    public record DeleteRecurringCommand(Guid Id) : IRequest;
}
