using FinanceCore.Application.Models;
using FinanceCore.Domain.RecurringTransaction;
using FinanceCore.Domain.Enums;
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
                AccountId = domain.AccountId,
                CategoryId = domain.CategoryId,
                Amount = domain.Amount.Amount,
                Currency = domain.Amount.Currency,
                Description = domain.Description,
                Type = domain.Type,
                StartDate = domain.StartDate,
                EndDate = domain.EndDate,
                Period = domain.Period,
                IsActive = domain.IsActive,
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
                model.EndDate
            );
            return recurring;
        }
    }
}