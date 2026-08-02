using FinanceCore.Application.DTOs.Transaction;

namespace FinanceCore.Application.Abstractions
{
    public interface ITransactionParser<T> where T : class
    {
        IEnumerable<T> Parse(Stream stream);
    }
}
