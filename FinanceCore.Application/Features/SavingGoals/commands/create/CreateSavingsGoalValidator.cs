using FinanceCore.Application.Features.Goals.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.create
{
    public class CreateSavingsGoalValidator : AbstractValidator<CreateSavingsGoalCommand>
    {
        public CreateSavingsGoalValidator() { }
    }
}
