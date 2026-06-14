using MediatR;
using System;

namespace FinanceCore.Application.Features.Recurring.Commands.Delete
{
    public record DeleteRecurringCommand(Guid userId ,Guid Id) : IRequest;
}
