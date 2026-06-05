using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.Pause
{
    public class PauseSavingGoalValidator : AbstractValidator<PauseSavingGoalCommand>
    {
        public PauseSavingGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
