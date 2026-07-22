using FluentValidation;

namespace FinanceCore.Application.Features.Profiles.Commands.Delete
{
    public class DeleteProfileValidator : AbstractValidator<DeleteProfileCommand>
    {
        public DeleteProfileValidator() { 
            RuleFor(x => x.id).NotEmpty();
       
        }
    }
}
