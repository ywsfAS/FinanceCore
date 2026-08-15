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
        decimal? InterestRate  = null,
        EnInterestAccrualFrequency? InterestAccrualFrequency = null,
        EnInterestCreditFrequency? InterestCreditFrequency = null ) : IRequest<AccountDto>;

}
