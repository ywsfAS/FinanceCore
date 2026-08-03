
using FinanceCore.Application.Models;
using System.Transactions;
using Z.Dapper.Plus;

namespace FinanceCore.Infrastructure.Configuration
{
    public class DapperPlusConfiguration
    {
        public static void Configure()
        {
            // Nap the entity to its sql table 
            DapperPlusManager.Entity<Transaction>().Table("Transactions");
            DapperPlusManager.Entity<TransactionModel>()
                .Table("Transactions")
                .Map(x => x.Id, "Id")
                .Map(x => x.AccountId, "AccountId")
                .Map(x => x.ToAccountId, "ToAccountId")
                .Map(x => x.Type, "TransactionTypeId")
                .Map(x => x.CategoryId, "CategoryId")
                .Map(x => x.Amount, "Amount")
                .Map(x => x.Currency, "CurrencyId")
                .Map(x => x.Date, "Date")
                .Map(x => x.Description, "Description")
                .Map(x => x.CreatedAt, "CreatedAt") 
                .Map(x => x.BatchId , "BatchId")
                .Map(x => x.UpdatedAt, "UpdatedAt");
        }
    }
}
