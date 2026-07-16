using FluentValidation;

namespace FinanceCore.Application.Features.Contact.Commands.Mark
{
    public class MarkContactMessageValidator : AbstractValidator<MarkContactMessageCommand>
    {
        public MarkContactMessageValidator() { 
            RuleFor(x => x.msgId).NotEmpty();
        }
    }
}
