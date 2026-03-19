using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Queries
{
    public class GetProfileByUserIdValidator : AbstractValidator<GetProfileByUserIdQuery>
    {
        public GetProfileByUserIdValidator() {
            RuleFor(x => x.userId).NotEmpty();
        }
    }
}
