using FluentValidation;

namespace FinanceCore.Application.Features.Profiles.Commands.Create
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Bio).NotEmpty();
            RuleFor(x => x.Curreny).NotEmpty().IsInEnum();
        }
    }
}
