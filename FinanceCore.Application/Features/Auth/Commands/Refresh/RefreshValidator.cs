using FluentValidation;

namespace FinanceCore.Application.Features.Auth.Commands.Refresh
{
    public class RefreshValidator : AbstractValidator<RefreshCommand>
    {
        public RefreshValidator() {
            RuleFor(x => x.refreshToken).NotEmpty(); 
        }
    }
}
