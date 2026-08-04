using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Abstractions
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid userId ,Guid id, CancellationToken cancellationToken = default);
        Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default);
        Task<Category?> GetCategoryByIdAndUserIdAsync(Guid userId , Guid id , CancellationToken token = default);
        Task<IEnumerable<CategoryDto>> GetFiltredCategoriesAsync(Guid userId, string? name, CategoryType? type, DateTime? createdAt, int page, int pageSize,CancellationToken token = default);
        Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default);
        Task<IEnumerable<CategoryOptionDto>> GetCategoriesByUserOptionsAsync(Guid userId ,int page ,int pageSize, CancellationToken token = default);
        Task<IDictionary<string,Guid>> ResolveCategoriesId(Guid userId,IEnumerable<string> names, CancellationToken token);
    }
}
