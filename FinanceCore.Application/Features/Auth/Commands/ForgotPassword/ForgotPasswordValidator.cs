using FluentValidation;

namespace FinanceCore.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator() {

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        
        }
    }
}
