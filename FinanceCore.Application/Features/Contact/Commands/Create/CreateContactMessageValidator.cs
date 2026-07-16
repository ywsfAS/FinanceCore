using FluentValidation;

namespace FinanceCore.Application.Features.Contact.Commands.Create
{
    public class CreateContactMessageValidator : AbstractValidator<CreateContactMessageCommand>
    {
        public CreateContactMessageValidator() {
            RuleFor(x => x.Email.Address).NotEmpty().MinimumLength(10).MaximumLength(200).EmailAddress();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Subject).NotEmpty().IsInEnum();
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
