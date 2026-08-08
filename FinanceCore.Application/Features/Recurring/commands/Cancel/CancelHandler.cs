using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Cancel
{
    public class CancelHandler : AbstractValidator<CancelCommand>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        public CancelHandler(IRecurringTransactionRepository recurringRepo)
        {
            _recurringTransactionRepository = recurringRepo;
        }
        public async Task Handle(CancelCommand command, CancellationToken token)
        {
            var recurring = await _recurringTransactionRepository.GetByIdAsync(command.UserId, command.Id);
            if (recurring is null) throw new RecurringTransactionNotFoundException(command.UserId, command.Id);
            recurring.MarkAsCanceled();
            await _recurringTransactionRepository.UpdateAsync(recurring, token: token);
        }
    }
}
