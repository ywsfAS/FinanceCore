
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
            DapperPlusManager.Entity<TransactionModel>().Table("Transactions");

        }
    }
}
