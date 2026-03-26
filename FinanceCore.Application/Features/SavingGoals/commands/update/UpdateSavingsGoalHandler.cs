using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Goals;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Goals.Commands.Update
{
    public class UpdateSavingsGoalHandler : IRequestHandler<UpdateSavingsGoalCommand, SavingsGoalDto>
    {
        private readonly ISavingsGoalRepository _goalRepository;
        private readonly IMediator _eventBus;

        public UpdateSavingsGoalHandler(ISavingsGoalRepository goalRepository, IMediator eventBus)
        {
            _goalRepository = goalRepository;
            _eventBus = eventBus;
        }

        public async Task<SavingsGoalDto> Handle(UpdateSavingsGoalCommand command, CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetByIdAsync(command.Id);
            if (goal == null)
                throw new InvalidOperationException("Savings goal not found.");

            // Update domain entity
            goal.UpdateDetails(
                command.Name,
                new Money(command.TargetAmount),
                command.TargetDate,
                command.Description,
                command.Status
            );

            await _goalRepository.UpdateAsync(goal);

            await DomainEventDispatcher.DispatchAsync(_eventBus, goal, cancellationToken);

            return new SavingsGoalDto(
                goal.Id,
                goal.UserId,
                goal.Name,
                goal.Description,
                goal.TargetAmount.Amount,
                goal.CurrentAmount.Amount,
                goal.TargetDate,
                goal.Status,
                goal.CreatedAt,
                goal.UpdatedAt,
                goal.CompletedAt
            );
        }
    }
}
