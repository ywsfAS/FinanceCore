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
                CurrencyId = (byte)domain.TargetAmount.Currency,
                TargetDate = domain.TargetDate,
                StatusId = (byte)domain.Status,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
                CompletedAt = domain.CompletedAt
            };
        }
        public static SavingsGoal MapToDomain(
    SavingsGoalModel model)
        {
            return SavingsGoal.Load(
                model.Id,
                model.UserId,
                model.Name,
                model.Description,
                new Money(
                    model.TargetAmount,
                    (EnCurrency)model.CurrencyId),
                new Money(
                    model.CurrentAmount,
                    (EnCurrency)model.CurrencyId),
                model.TargetDate,
                (EnGoalStatus)model.StatusId,
                model.CreatedAt,
                model.UpdatedAt,
                model.CompletedAt);
        }
    }
}