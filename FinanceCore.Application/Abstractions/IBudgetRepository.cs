using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IBudgetRepository
    {
        Task<Budget?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end);
        Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(Budget budget, CancellationToken cancellationToken = default);
        Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default);
        Task DeleteAsync(Budget budget, CancellationToken cancellationToken = default);
    }
}
