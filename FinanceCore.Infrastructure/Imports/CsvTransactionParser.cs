using CsvHelper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using System.Globalization;
using System.Reflection;

namespace FinanceCore.Infrastructure.Imports;

public sealed class CsvTransactionParser : ITransactionParser<TransactionImport>
{
    private static readonly IReadOnlyCollection<string> HeaderList =
        typeof(TransactionImport)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static)
            .Select(field => field.Name)
            .ToList();

    public IEnumerable<TransactionImport> Parse(Stream stream)
    {
        using var reader = new StreamReader(stream);

        using var csv = new CsvReader(reader,CultureInfo.InvariantCulture);

        var transactions = csv.GetRecords<TransactionImport>().ToList();

        return transactions;
    }

}
