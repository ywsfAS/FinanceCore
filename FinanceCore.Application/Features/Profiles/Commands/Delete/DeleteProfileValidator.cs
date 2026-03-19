using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.Delete
{
    public class DeleteProfileValidator : AbstractValidator<DeleteProfileCommand>
    {
        public DeleteProfileValidator() { 
            RuleFor(x => x.id).NotEmpty();
       
        }
    }
}
