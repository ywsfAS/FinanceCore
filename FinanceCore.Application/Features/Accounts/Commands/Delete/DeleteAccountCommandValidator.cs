using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Commands.Delete
{
    public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

}
