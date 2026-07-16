using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

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

        public Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId, Guid id , CancellationToken token)
        {
            var key = $"CategoryDto_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetDtoCategoryByIdAndUserIdAsync(userId, id,token);
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
        public Task<IEnumerable<CategoryDto>> GetFiltredCategoriesAsync(Guid userId, string? name, CategoryType? type, DateTime? createdAt, int page, int pageSize , CancellationToken token)
        {
            var key = $"CategoriesDto_Filtered_{userId}_{name}_{type}_{createdAt}_Page_{page}_Size_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetFiltredCategoriesAsync(userId, name, type, createdAt, page, pageSize,token);
            });
        }
        public Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"CategoryExists_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.IsExistsAsync(userId, id, token);
            });
        }

        public Task<IEnumerable<CategoryOptionDto>> GetCategoriesByUserOptionsAsync(Guid userId,int page , int pageSize , CancellationToken token)
        {
            var key = $"Category_{userId}_options";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _categoryRepository.GetCategoriesByUserOptionsAsync(userId,page,pageSize,token);
            });
            
        }

    }
}
