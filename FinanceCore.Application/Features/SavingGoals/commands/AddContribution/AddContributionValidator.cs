using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.AddContribution
{
    public class AddContributionValidator : AbstractValidator<AddContributionCommand>
    {
        public AddContributionValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.GoalId).NotEmpty();
            RuleFor(x => x.ContributionDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
        }
    }
}
