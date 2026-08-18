
using FluentValidation;

namespace FinanceCore.Application.Features.Users.Command.AssignRole
{
    public class AssignRoleValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
        
        }
    }
}
