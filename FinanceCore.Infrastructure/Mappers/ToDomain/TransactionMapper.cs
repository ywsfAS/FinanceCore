using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    public static class TransactionMapper
    {
        public static Transaction MapToDomain(TransactionModel model)
        {

            return Transaction.Create(model.Id , model.AccountId , MoneyMapper.MapToDomain(model.Amount),model.CategoryId,(EnTransactionType)model.Type,model.Date,model.Description,null);

        }
    }
}
