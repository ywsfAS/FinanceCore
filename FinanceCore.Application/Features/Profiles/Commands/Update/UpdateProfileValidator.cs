using FluentValidation;

namespace FinanceCore.Application.Features.Profiles.Commands.Update
{
     public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Bio).NotEmpty();
            RuleFor(x => x.Currency).IsInEnum();
        }
    }
}
