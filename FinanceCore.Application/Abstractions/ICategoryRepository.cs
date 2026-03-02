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
        Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
        Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid UserId, Guid Id);
        Task<Category?> GetCategoryByIdAndUserIdAsync(Guid UserId , Guid Id );
        Task<IEnumerable<CategoryDto>?> GetCategoriesByUserIdAsync(Guid? UserId, int Page, int PageSize);
        Task<IEnumerable<CategoryDto>?> GetFiltredCategoriesAsync(Guid? UserId, string? Name, byte? Type, DateTime? CreatedAt, int Page, int PageSize);
    }
}
