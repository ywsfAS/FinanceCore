using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Update;

public record UpdateAccountCommand(
    Guid UserId,
    Guid AccountId,
    string Name,
    EnAccountType AccountType,
    decimal? InterestRate = null,
    EnInterestAccrualFrequency? AccrualFrequency = null,
    EnInterestCreditFrequency? CreditFrequency = null
) : IRequest;
