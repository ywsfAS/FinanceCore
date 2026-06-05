using FinanceCore.Application.Models;
using FinanceCore.Domain.RecurringTransaction;
using FinanceCore.Domain.Enums;
using System;
using FinanceCore.Domain.Common;

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
                Amount = domain.amount.Amount,
                Currency = (byte)domain.amount.Currency,
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
                new Money(model.Amount,(EnCurrency)model.Currency),
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