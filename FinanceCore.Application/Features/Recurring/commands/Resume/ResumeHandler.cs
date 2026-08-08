using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Resume
{
    public class ResumeHandler : IRequestHandler<ResumeCommand>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        public ResumeHandler(IRecurringTransactionRepository recurringRepo)
        {
            _recurringTransactionRepository = recurringRepo;
        }
        public async Task Handle(ResumeCommand command, CancellationToken token)
        {
            var recurring = await _recurringTransactionRepository.GetByIdAsync(command.UserId, command.Id);
            if (recurring is null) throw new RecurringTransactionNotFoundException(command.UserId, command.Id);
            recurring.MarkAsResumed();
            await _recurringTransactionRepository.UpdateAsync(recurring, token: token);
        }
    }
}
