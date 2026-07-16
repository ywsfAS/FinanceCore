using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Abstractions
{
    public interface IBudgetRepository
    {
        Task<Budget?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end,CancellationToken token);
        Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BudgetDto>?> GetDtoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<BudgetDto?> GetDtoByIdAndUserIdAsync(Guid userId , Guid id , CancellationToken token);
        Task<Budget?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token);
        Task AddAsync(Budget budget, CancellationToken cancellationToken = default);
        Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default);
        Task<IEnumerable<BudgetInfoDto>?> GetBudgetsFilteredAsync(Guid userId , string? name , Guid? categoryId , EnPeriod? period , int page  , int pageSize, CancellationToken token);
    }
}
