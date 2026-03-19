using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.Create
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileValidator() {
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.avatarUrl).NotEmpty();
            RuleFor(x => x.bio).NotEmpty();
            RuleFor(x => x.curreny).NotEmpty().IsInEnum();
        }
    }
}
