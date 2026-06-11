using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheBudgetRepository : IBudgetRepository
    {
        private readonly BudgetRepository _budgetRepository;
        private readonly IMemoryCache _memoryCache;
        public CacheBudgetRepository(BudgetRepository budgetRepository, IMemoryCache memoryCache)
        {
            _budgetRepository = budgetRepository;
            _memoryCache = memoryCache;
        }
        public Task<Budget?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string key = $"Budget_{id}";
            return _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _budgetRepository.GetByIdAsync(id, cancellationToken);
            });


        }
        public Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end, CancellationToken token)
        {
            string key = $"Budget_Category_{userId}_{categoryId}_{start:yyyyMMdd}_{end:yyyyMMdd}";
            return _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _budgetRepository.GetByCategoryIdAsync(userId,categoryId,start,end,token);
            });


        }
       public Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            string key = $"Budgets_User_{userId}";
            return _memoryCache.GetOrCreateAsync(key,entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _budgetRepository.GetByUserIdAsync(userId, cancellationToken);
            });

        }
        public async Task<IEnumerable<BudgetInfoDto>?> GetBudgetsFilteredAsync(
            Guid userId,
            string? name,
            Guid? categoryId,
            EnPeriod? period,
            int page = 1,
            int pageSize = 10,
            CancellationToken token = default)
        {
            string key = $"budgets:filtered:{userId}:{name}:{categoryId}:{period}:{page}:{pageSize}";

            if (_memoryCache.TryGetValue(key, out IEnumerable<BudgetInfoDto>? cached))
                return cached;

            var result = await _budgetRepository.GetBudgetsFilteredAsync(
                userId,
                name,
                categoryId,
                period,
                page,
                pageSize,
                token
            );

            _memoryCache.Set(
                key,
                result,
                TimeSpan.FromMinutes(10)
            );

            return result;
        }
        public Task<IEnumerable<BudgetDto>?> GetDtoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            string key = $"BudgetDtos_User_{userId}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _budgetRepository.GetDtoByUserIdAsync(userId, cancellationToken);
            });

        }
        public Task<BudgetDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            string key = $"BudgetDto_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _budgetRepository.GetDtoByIdAndUserIdAsync(userId, id, token);
            });

        }
        public Task<Budget?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            string key = $"Budget_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _budgetRepository.GetByIdAndUserIdAsync(userId, id, token);
            });

        }
        public Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            return _budgetRepository.AddAsync(budget, cancellationToken);

        }
        public Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            return _budgetRepository.UpdateAsync(budget, cancellationToken);
        }
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _budgetRepository.DeleteAsync(id, cancellationToken);
        }
        public Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"BudgetExists_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _budgetRepository.IsExists(userId, id, token);
            });
            
        }

    }
}
