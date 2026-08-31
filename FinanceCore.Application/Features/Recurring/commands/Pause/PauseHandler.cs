using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Pause
{
    public class PauseHandler : IRequestHandler<PauseCommand>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        public PauseHandler(IRecurringTransactionRepository recurringRepo)
        {
            _recurringTransactionRepository = recurringRepo;
        }
        public async Task Handle(PauseCommand command , CancellationToken token)
        {
            var recurring = await _recurringTransactionRepository.GetByIdAsync(command.UserId,command.Id);
            if (recurring is null) throw new RecurringTransactionNotFoundException(command.UserId,command.Id);
            recurring.MarkAsPaused();
            await _recurringTransactionRepository.UpdateAsync(recurring,token: token);
        }
    }
}
