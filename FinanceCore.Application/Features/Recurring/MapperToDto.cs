using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.RecurringTransaction;

namespace FinanceCore.Application.Features.Recurring
{
    public static class MapperToDto
    {
        public static RecurringTransactionDto MapToDto(RecurringTransaction r) => new()
        {
            Id = r.Id,
            AccountId = r.AccountId,
            CategoryId = r.CategoryId,
            Amount = r.Amount.Amount,
            Currency = r.Amount.Currency,
            Description = r.Description,
            Type = r.Type,
            Period = r.Period,
            StartDate = r.StartDate,
            EndDate = r.EndDate
        };
    }
}
