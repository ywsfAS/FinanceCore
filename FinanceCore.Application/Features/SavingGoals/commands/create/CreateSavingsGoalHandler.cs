using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.Events;
using FinanceCore.Domain;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Goals;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Goals.Commands.Create
{
    public class CreateSavingsGoalHandler : IRequestHandler<CreateSavingsGoalCommand, SavingsGoalDto>
    {
        private readonly ISavingsGoalRepository _goalRepository;
        private readonly IMediator _eventBus;

        public CreateSavingsGoalHandler(
            ISavingsGoalRepository goalRepository,
            IMediator eventBus)
        {
            _goalRepository = goalRepository;
            _eventBus = eventBus;
        }

        public async Task<SavingsGoalDto> Handle(CreateSavingsGoalCommand command, CancellationToken cancellationToken)
        {
            var goal = SavingsGoal.Create(
                null,
                command.UserId,
                command.Name,
                new Money(command.TargetAmount),
                command.TargetDate,
                command.Description
            );

            await _goalRepository.AddAsync(goal);

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
