using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Repositories;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheCategoryRepository : ICategoryRepository
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ICacheService _cache;

        private static string Tag(Guid userId) => $"Categories_{userId}";

        public CacheCategoryRepository(CategoryRepository categoryRepository, ICacheService cache)
        {
            _categoryRepository = categoryRepository;
            _cache = cache;
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.AddAsync(category, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(category.UserId));
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.UpdateAsync(category, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(category.UserId));
        }

        public async Task DeleteAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.DeleteAsync(userId,id, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(userId));
        }

        public Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            var key = $"CategoryDto_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _categoryRepository.GetDtoCategoryByIdAndUserIdAsync(userId, id, token));
        }

        public Task<Category?> GetCategoryByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            var key = $"Category_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _categoryRepository.GetCategoryByIdAndUserIdAsync(userId, id, token));
        }

        public Task<IEnumerable<CategoryDto>> GetFiltredCategoriesAsync(Guid userId, string? name, CategoryType? type, DateTime? createdAt, int page, int pageSize, CancellationToken token)
        {
            var key = $"CategoriesDto_Filtered_{name}_{type}_{createdAt}_Page_{page}_Size_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _categoryRepository.GetFiltredCategoriesAsync(userId, name, type, createdAt, page, pageSize, token));
        }

        public Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"CategoryExists_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _categoryRepository.IsExistsAsync(userId, id, token));
        }

        public Task<IEnumerable<CategoryOptionDto>> GetCategoriesByUserOptionsAsync(Guid userId, int page, int pageSize, CancellationToken token)
        {
            var key = $"CategoryOptions_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _categoryRepository.GetCategoriesByUserOptionsAsync(userId, page, pageSize, token));
        }
    }
}