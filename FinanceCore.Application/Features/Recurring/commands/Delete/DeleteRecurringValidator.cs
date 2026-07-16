using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Delete
{
    public class DeleteRecurringValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteRecurringValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
} 
