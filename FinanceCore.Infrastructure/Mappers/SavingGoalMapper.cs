using FinanceCore.Application.Models;
using FinanceCore.Domain.Goals;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Common;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class SavingsGoalMapper
    {
        public static SavingsGoalModel MapToModel(SavingsGoal domain)
        {
            return new SavingsGoalModel
            {
                Id = domain.Id,
                UserId = domain.UserId,
                Name = domain.Name,
                Description = domain.Description,
                TargetAmount = domain.TargetAmount.Amount,
                CurrentAmount = domain.CurrentAmount.Amount,
                TargetDate = domain.TargetDate,
                Status = domain.Status,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
                CompletedAt = domain.CompletedAt
            };
        }

        public static SavingsGoal MapToDomain(SavingsGoalModel model)
        {
            var goal = SavingsGoal.Create(
                model.Id,
                model.UserId,
                model.Name,
                new Money(model.TargetAmount),
                model.TargetDate,
                model.Description
            );
            if (model.CurrentAmount > 0)
            {
                goal.AddContribution(new Money(model.CurrentAmount));
            }

            if (model.Status == EnGoalStatus.Completed)
            {
                goal.AddContribution(new Money(0));  
            }

            return goal;
        }
    }
}