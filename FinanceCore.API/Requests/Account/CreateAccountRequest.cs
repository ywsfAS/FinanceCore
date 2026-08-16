using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Account
{
    public record CreateAccountRequest(string Name , EnAccountType Type , EnCurrency Currency , decimal InitialBalance = 0, decimal? InterestRate = null,
        EnInterestAccrualFrequency? InterestAccrualFrequency = null,
        EnInterestCreditFrequency? InterestCreditFrequency = null,
        decimal? CreditLimit = null,
        decimal? Fee = null,
        EnPeriod? FeePeriod = null
        );
}
