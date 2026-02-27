using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid id, CancellationToken token = default);
        Task<TransferDto> TransferAsync(Transaction transaction, CancellationToken token = default);
        Task<IncomeDto> IncomeAsync(Transaction transaction, CancellationToken token);

        Task AddAsync(Transaction transaction, CancellationToken token = default);
        Task UpdateAsync(Transaction transaction, CancellationToken token = default);
        Task DeleteAsync(Transaction transaction, CancellationToken token = default);

    }
}
