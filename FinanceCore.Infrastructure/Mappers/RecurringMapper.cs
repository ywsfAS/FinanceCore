using FinanceCore.Application.Models;
using FinanceCore.Domain.RecurringTransaction;
using FinanceCore.Domain.Enums;
using System;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class RecurringTransactionMapper
    {
        public static RecurringTransactionModel MapToModel(RecurringTransaction domain)
        {
            return new RecurringTransactionModel
            {
                Id = domain.Id,
                AccountId = domain.accountId,
                CategoryId = domain.categoryId,
                Amount = domain.amount,
                Description = domain.description,
                Type = domain.type,
                StartDate = domain.startDate,
                EndDate = domain.endDate,
                Period = domain.period,
                Interval = domain.interval,
                IsActive = domain.isActive,
                LastExecutedDate = domain.LastExecutedDate
            };
        }

        public static RecurringTransaction MapToDomain(RecurringTransactionModel model)
        {
            var recurring = RecurringTransaction.Create(
                model.AccountId,
                model.CategoryId,
                model.Amount,
                model.Description,
                model.Type,
                model.StartDate,
                model.Period,
                model.Interval,
                model.EndDate
            );

            if (!model.IsActive)
                recurring.deactivate();

            if (model.LastExecutedDate.HasValue)
                recurring.markAsExecuted(model.LastExecutedDate.Value);

            return recurring;
        }
    }
}