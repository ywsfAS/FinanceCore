using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Events;
using FinanceCore.Domain.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.RecurringTransaction.Commands.Delete
{
    public class DeleteRecurringHandler : IRequestHandler<DeleteRecurringCommand>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        private readonly IMediator _eventBus;

        public DeleteRecurringHandler(
            IRecurringTransactionRepository recurringRepository,
            IMediator eventBus)
        {
            _recurringRepository = recurringRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(DeleteRecurringCommand command, CancellationToken cancellationToken)
        {
            // Load existing entity
            var recurring = await _recurringRepository.GetByIdAsync(command.Id);
            if (recurring == null)
                throw new InvalidOperationException("Recurring transaction not found.");

            // Remove from repository
            await _recurringRepository.DeleteAsync(command.Id);

            // dispatch event
            await DomainEventDispatcher.DispatchAsync(_eventBus, recurring, cancellationToken);

           
        }
    }
}
