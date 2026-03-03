using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId, Guid id);
        Task<Category?> GetCategoryByIdAndUserIdAsync(Guid userId , Guid id , CancellationToken token );
        Task<IEnumerable<CategoryDto>?> GetCategoriesByUserIdAsync(Guid? userId, int page, int pageSize , CancellationToken token);
        Task<IEnumerable<CategoryDto>?> GetFiltredCategoriesAsync(Guid? userId, string? name, byte? type, DateTime? createdAt, int page, int pageSize);
        Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default);
    }
}
