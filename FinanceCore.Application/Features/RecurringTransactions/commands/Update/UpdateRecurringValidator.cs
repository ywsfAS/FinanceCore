using FinanceCore.Application.Features.RecurringTransaction.Commands.Update;
using FluentValidation;

namespace FinanceCore.Application.Features.RecurringTransactions.commands.Update
{
    public class UpdateRecurringValidator : AbstractValidator<UpdateRecurringCommand>
    {
        public UpdateRecurringValidator() { }
    }
}
