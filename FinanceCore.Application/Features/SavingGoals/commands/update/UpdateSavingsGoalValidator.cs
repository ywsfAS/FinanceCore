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
        public UpdateSavingsGoalValidator() { }
    }
}
