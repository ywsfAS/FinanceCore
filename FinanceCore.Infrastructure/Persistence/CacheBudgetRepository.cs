using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Repositories;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheBudgetRepository : IBudgetRepository
    {
        private readonly BudgetRepository _budgetRepository;
        private readonly ICacheService _cache;

        private static string Tag(Guid userId) => $"Budgets_{userId}";

        public CacheBudgetRepository(BudgetRepository budgetRepository, ICacheService cache)
        {
            _budgetRepository = budgetRepository;
            _cache = cache;
        }

        // No userId available here — can't scope by user tag. See note below.
        public Task<Budget?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _budgetRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end, CancellationToken token)
        {
            string key = $"Budget_Category_{categoryId}_{start:yyyyMMdd}_{end:yyyyMMdd}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _budgetRepository.GetByCategoryIdAsync(userId, categoryId, start, end, token));
        }

        public Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            string key = "Budgets_ByUser";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _budgetRepository.GetByUserIdAsync(userId, cancellationToken));
        }

        public Task<IEnumerable<BudgetInfoDto>?> GetBudgetsFilteredAsync(
            Guid userId, string? name, Guid? categoryId, EnPeriod? period, int page = 1, int pageSize = 10, CancellationToken token = default)
        {
            string key = $"BudgetsFiltered_{name}_{categoryId}_{period}_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key,
                () => _budgetRepository.GetBudgetsFilteredAsync(userId, name, categoryId, period, page, pageSize, token));
        }

        public Task<IEnumerable<BudgetDto>?> GetDtoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            string key = "BudgetDtos_ByUser";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _budgetRepository.GetDtoByUserIdAsync(userId, cancellationToken));
        }

        public Task<BudgetDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            string key = $"BudgetDto_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _budgetRepository.GetDtoByIdAndUserIdAsync(userId, id, token));
        }

        public Task<Budget?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            string key = $"Budget_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _budgetRepository.GetByIdAndUserIdAsync(userId, id, token));
        }

        public Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"BudgetExists_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _budgetRepository.IsExists(userId, id, token));
        }

        public async Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            await _budgetRepository.AddAsync(budget, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(budget.UserId));
        }

        public async Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            await _budgetRepository.UpdateAsync(budget, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(budget.UserId));
        }

        public async Task DeleteAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
        {
            await _budgetRepository.DeleteAsync(userId, id, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(userId));
        }
    }
}