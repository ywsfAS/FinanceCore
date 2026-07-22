using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Domain.Goals
{
    public class SavingsContribution : Entity
    {
        public Guid SavingGoalId { get; private set; }
        public Guid AccountId { get; private set; }
        public Money Amount { get; private set; }
        public SavingsType Type { get; private set; } = SavingsType.Contribution;
        public string? Description { get; private set; }
        public DateTime Date {  get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
    
        public SavingsContribution() { }
        public static SavingsContribution Create(
           Guid savingGoalId,
           Guid accountId,
           Money amount,
           DateTime date,
           string? description = null,
           DateTime? updatedAt = null,
           SavingsType type = SavingsType.Contribution 
            )
        {
            if(savingGoalId == Guid.Empty) throw new GoalNotFoundException(savingGoalId);
            if(accountId == Guid.Empty) throw new AccountNotFoundException(accountId);
            if (amount == null || amount.IsLessOrEqual(Money.Zero(amount.Currency))) throw new MoneyIsNegativeException();
            if(date == DateTime.MinValue) throw new ArgumentException("Date is not valid");


            return new SavingsContribution
            {
                SavingGoalId = savingGoalId,
                AccountId = accountId,
                Amount = amount,
                Description = description,
                Date = date,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = updatedAt,
                Type = type
            };
        }
        
    }
}
