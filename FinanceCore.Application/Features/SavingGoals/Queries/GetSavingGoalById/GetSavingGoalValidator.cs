using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalById
{
    public class GetSavingGoalValidator : AbstractValidator<GetSavingGoalQuery>
    {
        public GetSavingGoalValidator() { }
    }
}
