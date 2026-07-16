
using FluentValidation;

namespace FinanceCore.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);
            RuleFor(x => x.Token)
                .NotEmpty();

        }
    }
}
