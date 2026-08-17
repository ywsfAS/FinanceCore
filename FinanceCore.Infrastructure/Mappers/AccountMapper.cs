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
            UpdatedAt = account.UpdatedAt ?? account.CreatedAt,
            RowVersion = account.RowVersion
        };

        if (account.CreditDetails is not null)
        {
            model.CreditLimit =
                account.CreditDetails.CreditLimit.Amount;

            model.Fee =
                account.CreditDetails.Fee.Amount;

            model.FeePeriod =
                account.CreditDetails.FeePeriod;

            model.LastFeeChargedAt =
                account.CreditDetails.LastFeeChargedAt;

            model.NextFeeChargeAt =
                account.CreditDetails.NextFeeChargeAt;
        }

        if (account.SavingsDetails is not null)
        {
            model.InterestRate =
                account.SavingsDetails.InterestRate;

            model.InterestAccruedToDate =
                account.SavingsDetails.InterestAccruedToDate.Amount;

            model.AccrualFrequency =
                account.SavingsDetails.AccrualFrequency;

            model.CreditFrequency =
                account.SavingsDetails.CreditFrequency;

            model.LastInterestAccrualAt =
                account.SavingsDetails.LastInterestAccrualAt;

            model.NextInterestAccrualAt =
                account.SavingsDetails.NextInterestAccrualAt;

            model.LastInterestCreditAt =
                account.SavingsDetails.LastInterestCreditAt;

            model.NextInterestCreditAt =
                account.SavingsDetails.NextInterestCreditAt;
        }

        return model;
    }

    public static Account MapToDomain(AccountModel model)
    {
        SavingsDetails? savingsDetails = null;
        CreditDetails? creditDetails = null;

        var currency = (EnCurrency)model.CurrencyId;

        if (model.AccountTypeId == EnAccountType.Savings)
        {
            savingsDetails = SavingsDetails.Load(
                model.InterestRate!.Value,
                new Money(
                    model.InterestAccruedToDate!.Value,
                    currency),
                model.CreditFrequency!.Value,
                model.AccrualFrequency!.Value,
                model.LastInterestAccrualAt,
                model.NextInterestAccrualAt!.Value,
                model.LastInterestCreditAt,
                model.NextInterestCreditAt!.Value);
        }

        if (model.AccountTypeId == EnAccountType.Credit)
        {
            var creditLimit = new Money(
                model.CreditLimit!.Value,
                currency);

            var fee = new Money(
                model.Fee!.Value,
                currency);

            creditDetails = CreditDetails.Load(
                creditLimit,
                fee,
                model.FeePeriod,
                model.LastFeeChargedAt,
                model.NextFeeChargeAt);
        }

        return Account.Load(
            model.Id,
            model.UserId,
            model.Name,
            model.AccountTypeId,
            new Money(
                model.Balance,
                currency),
            new Money(
                model.InitialBalance,
                currency),
            model.IsActive,
            model.CreatedAt,
            model.UpdatedAt,
            savingsDetails,
            creditDetails,
            null,
            model.RowVersion);
    }
}