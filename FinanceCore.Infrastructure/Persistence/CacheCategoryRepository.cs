using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Categories;
using FinanceCore.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheCategoryRepository : ICategoryRepository
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IMemoryCache _memoryCache;
        public CacheCategoryRepository(CategoryRepository categoryRepository, IMemoryCache memoryCache)
        {
            _categoryRepository = categoryRepository;
            _memoryCache = memoryCache;
        }

        public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var key = $"Category_{id}";
            return _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _categoryRepository.GetByIdAsync(id, cancellationToken);
            });
        }
        public Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var key = $"Categories_User_{userId}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetByUserIdAsync(userId, cancellationToken);
            });
        }
        public Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            return _categoryRepository.AddAsync(category, cancellationToken);
        }
        public Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            return _categoryRepository.UpdateAsync(category, cancellationToken);
        }
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _categoryRepository.DeleteAsync(id, cancellationToken);
        }

        public Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId, Guid id)
        {
            var key = $"CategoryDto_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetDtoCategoryByIdAndUserIdAsync(userId, id);
            });
        }
        public Task<Category?> GetCategoryByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            var key = $"Category_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetCategoryByIdAndUserIdAsync(userId, id, token);
            });
        }
        public Task<IEnumerable<CategoryDto>?> GetCategoriesByUserIdAsync(Guid? userId, int page, int pageSize, CancellationToken token)
        {
            var key = $"CategoriesDto_User_{userId}_Page_{page}_Size_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetCategoriesByUserIdAsync(userId, page, pageSize, token);
            });
        }
        public Task<IEnumerable<CategoryDto>?> GetFiltredCategoriesAsync(Guid? userId, string? name, byte? type, DateTime? createdAt, int page, int pageSize)
        {
            var key = $"CategoriesDto_Filtered_{userId}_{name}_{type}_{createdAt}_Page_{page}_Size_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetFiltredCategoriesAsync(userId, name, type, createdAt, page, pageSize);
            });
        }
        public Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"CategoryExists_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.IsExists(userId, id, token);
            });
        }

    }
}
