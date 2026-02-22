using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Common;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class AccountMapper
    {
        public static AccountModel MapToModel(Account account)
        {
            return new AccountModel { Id = account.Id, UserId = account.UserId, Name = account.Name, Type = (byte)account.Type, Balance = account.Balance.Amount,BalanceCurrency = (byte)account.Balance.Currency ,InitialBalance = account.InitialBalance.Amount,InitialBalanceCurrency = (byte)account.InitialBalance.Currency, Currency = (byte)account.Currency, IsActive = account.IsActive, CreatedAt = account.CreatedAt};


        }
        public static Account MapToDomain(AccountModel model)
        {
            return Account.Create(model.Id, model.UserId, model.Name, (EnAccountType)model.Type, (EnCurrency)model.Currency, new Money(model.Balance,(EnCurrency)model.BalanceCurrency), new Money(model.InitialBalance,(EnCurrency)model.InitialBalanceCurrency), model.IsActive, model.CreatedAt, null, null);
        }
    }
}
