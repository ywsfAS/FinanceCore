using FinanceCore.Application.Features.Goals.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.update
{
    public class UpdateSavingsGoalValidator : AbstractValidator<UpdateSavingsGoalCommand>
    {
        public UpdateSavingsGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TargetAmount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.TargetDate).GreaterThan(DateTime.UtcNow);
        }
    }
}
