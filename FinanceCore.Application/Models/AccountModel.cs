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

    // Credit details
    public decimal? CreditLimit { get; set; }
    public decimal? Fee { get; set; }
    public EnPeriod FeePeriod { get; set; }
    public DateTime? LastFeeChargedAt { get; set; }
    public DateTime? NextFeeChargeAt { get; set; }

    // Loan details
    public decimal? LoanPrincipalAmount { get; set; }
    public decimal? LoanInterestRate { get; set; }
    public int? LoanTermInMonths { get; set; }
    public EnRepaymentFrequency? LoanRepaymentFrequency { get; set; }
    public DateTime? LoanStartDate { get; set; }
    public decimal? LoanRegularPaymentAmount { get; set; }
    public DateTime? LoanMaturityDate { get; set; }
    public DateTime? LoanNextPaymentDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public byte[]? RowVersion { get; set; }
}