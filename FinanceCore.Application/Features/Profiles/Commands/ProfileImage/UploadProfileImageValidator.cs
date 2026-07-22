using FluentValidation;

namespace FinanceCore.Application.Features.Profiles.Commands.ProfileImage
{
    public class UploadProfileImageValidator : AbstractValidator<UploadProfileImageCommand>
    {
        public UploadProfileImageValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FileName).NotEmpty();
        }
    }
}
