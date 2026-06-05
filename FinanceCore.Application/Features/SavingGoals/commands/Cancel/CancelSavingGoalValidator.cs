using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.Cancel
{
    public class CancelSavingGoalValidator : AbstractValidator<CancelSavingGoalCommand>
    {
        public CancelSavingGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
