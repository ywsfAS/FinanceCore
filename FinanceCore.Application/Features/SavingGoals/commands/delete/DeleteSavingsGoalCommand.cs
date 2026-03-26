using MediatR;
using System;

namespace FinanceCore.Application.Features.Goals.Commands.Delete
{
    public record DeleteSavingsGoalCommand(Guid Id) : IRequest;
}

