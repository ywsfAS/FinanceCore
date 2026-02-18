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
        Task AddAsync(Transaction transaction, CancellationToken token = default);
        Task UpdateAsync(Transaction transaction, CancellationToken token = default);
        Task DeleteAsync(Transaction transaction, CancellationToken token = default);

    }
}
