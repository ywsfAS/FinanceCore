using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
