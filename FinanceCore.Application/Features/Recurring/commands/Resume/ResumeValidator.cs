
using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Resume
{
    public class ResumeValidator : AbstractValidator<ResumeCommand>
    {
        public ResumeValidator() 
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}
