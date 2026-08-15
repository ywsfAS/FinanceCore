using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;

    public EnAccountType AccountTypeId { get; set; }

    public decimal Balance { get; set; }
    public decimal InitialBalance { get; set; }
    public byte CurrencyId { get; set; }

    // Savings details
    public decimal? InterestRate { get; set; }

    public decimal? InterestAccruedToDate { get; set; }

    public EnInterestAccrualFrequency? AccrualFrequency { get; set; }

    public EnInterestCreditFrequency? CreditFrequency { get; set; }

    public DateTime? LastInterestAccrualAt { get; set; }

    public DateTime? NextInterestAccrualAt { get; set; }

    public DateTime? LastInterestCreditAt { get; set; }

    public DateTime? NextInterestCreditAt { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public byte[]? RowVersion { get; set; }
}