using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    public static class AccountMapper
    {
        public static Account MapToDomain(AccountModel model)
        {
            return Account.Create(model.Id, model.UserId, model.Name, (EnAccountType)model.Type, (EnCurrency)model.Currency,MoneyMapper.MapToDomain(model.Balance),MoneyMapper.MapToDomain(model.InitialBalance), model.IsActive, model.CreatedAt,null, null);
        }
    }
}
