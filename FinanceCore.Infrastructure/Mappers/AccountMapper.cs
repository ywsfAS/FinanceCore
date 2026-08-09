using FinanceCore.Application.Models;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Infrastructure.Mappers;

public static class AccountMapper
{
    public static AccountModel MapToModel(Account account)
    {
        var model = new AccountModel
        {
            Id = account.Id,
            UserId = account.UserId,
            Name = account.Name,
            AccountTypeId = account.Type,
            Balance = account.Balance.Amount,
            InitialBalance = account.InitialBalance.Amount,
            CurrencyId = (byte)account.Balance.Currency,
            IsActive = account.IsActive,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt.Value,
            RowVersion = account.RowVersion
        };

        if (account.SavingsDetails is not null)
        {
            model.InterestRate = account.SavingsDetails.InterestRate;
            model.InterestAccruedToDate =
                account.SavingsDetails.InterestAccruedToDate.Amount;
            model.CreditFrequency =
                account.SavingsDetails.CreditFrequency;
            model.LastInterestAccrualAt =
                account.SavingsDetails.LastInterestAccrualAt;
            model.NextInterestCreditAt =
                account.SavingsDetails.NextInterestCreditAt;
        }

        return model;
    }

    public static Account MapToDomain(AccountModel model)
    {
        SavingsDetails? savingsDetails = null;

        if (model.AccountTypeId == EnAccountType.Savings)
        {
            savingsDetails = SavingsDetails.Load(
                model.InterestRate!.Value,
                new Money(
                    model.InterestAccruedToDate!.Value,
                    (EnCurrency)model.CurrencyId),
                model.CreditFrequency!.Value,
                model.LastInterestAccrualAt,
                model.NextInterestCreditAt);
        }

        return Account.Load(
            model.Id,
            model.UserId,
            model.Name,
            model.AccountTypeId,
            new Money(
                model.Balance,
                (EnCurrency)model.CurrencyId),
            new Money(
                model.InitialBalance,
                (EnCurrency)model.CurrencyId),
            model.IsActive,
            model.CreatedAt,
            model.UpdatedAt,
            savingsDetails,
            model.RowVersion);
    }
}
