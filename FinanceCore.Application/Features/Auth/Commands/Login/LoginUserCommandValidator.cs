using FluentValidation;

namespace FinanceCore.Application.Features.Auth.Commands.Login
{
   public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator() { 
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(128);
       
        }
    }
}
