using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Create
{
    public record CreateAccountCommand(
        Guid UserId,
        string Name,
        EnAccountType Type,
        Money InitialBalance,
        decimal? InterestRate = null,
        EnInterestAccrualFrequency? InterestAccrualFrequency = null,
        EnInterestCreditFrequency? InterestCreditFrequency = null,
        decimal? CreditLimit = null,
        decimal? Fee = null,
        EnPeriod? FeePeriod = null,
        decimal? PrincipalAmount = null,
        decimal? LoanInterestRate = null,
        int? TermInMonths = null,
        EnRepaymentFrequency? RepaymentFrequency = null,
        DateTime? StartDate = null,
        decimal? RegularPaymentAmount = null,
        DateTime? MaturityDate = null,
        DateTime? NextPaymentDate = null
        ) : IRequest<AccountDto>;

}
