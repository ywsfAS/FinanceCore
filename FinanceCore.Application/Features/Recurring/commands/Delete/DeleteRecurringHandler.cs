using FinanceCore.Application.Abstractions;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Delete
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
            // Remove from repository
            await _recurringRepository.DeleteAsync(command.Id);
         
        }
    }
}
