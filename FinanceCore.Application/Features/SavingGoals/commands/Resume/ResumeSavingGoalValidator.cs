using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.Resume
{
    public class ResumeSavingGoalValidator : AbstractValidator<ResumeSavingGoalCommand>
    {
        public ResumeSavingGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        
        }
    }
}
