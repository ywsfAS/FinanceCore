using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using System.Text;

namespace FinanceCore.Infrastructure.Services
{
    public class TransactionExporter : ITransactionExporter
    {
        public ExportCSVDto ExportCSV(IEnumerable<TransactionDto> transactions)
        {
           var csv = new StringBuilder();
            // Append table header
            csv.AppendLine("Id,Account,To Account,Category,Amount,Currency,Type,Date,Description");
            foreach (var transaction in transactions) {
                csv.AppendLine(
                $"{transaction.Id}," +
                $"{Escape(transaction.AccountName)}," +
                $"{Escape(transaction.ToAccountName)}," +
                $"{Escape(transaction.CategoryName)}," +
                $"{transaction.Amount}," +
                $"{transaction.Currency}," +
                $"{transaction.Type}," +
                $"{transaction.Date:yyyy-MM-dd HH:mm:ss}," +
                $"{Escape(transaction.Description)}");
            }
            return new ExportCSVDto(Encoding.UTF8.GetBytes(csv.ToString()),
            $"transactions-{DateTime.UtcNow:yyyyMMddHHmmss}.csv","text/csv");
        }
        private static string Escape(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return $"\"{value.Replace("\"", "\"\"")}\"";

            return value;
        }
    }

}
