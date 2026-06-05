using FinanceCore.Application.Models;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class AccountMapper
    {
        public static AccountModel MapToModel(Account account)
        {
            return new AccountModel { Id = account.Id, UserId = account.UserId, Name = account.Name, AccountTypeId = (byte)account.Type, Balance = account.Balance.Amount ,InitialBalance = account.InitialBalance.Amount, CurrencyId = (byte)account.Balance.Currency, IsActive = account.IsActive, CreatedAt = account.CreatedAt};


        }
        public static Account MapToDomain(AccountModel model)
        {
            return Account.Create(model.Id, model.UserId, model.Name, (EnAccountType)model.AccountTypeId,new Money(model.Balance,(EnCurrency)model.CurrencyId), new Money(model.InitialBalance,(EnCurrency)model.CurrencyId), model.IsActive, model.CreatedAt, model.UpdatedAt);
        }
    }
}
