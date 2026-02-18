using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Categories;
using FinanceCore.Infrastructure.context;

namespace FinanceCore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CategoryRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _connectionFactory.ReadSingleAsync<Category, Guid>(
                "sp_GetCategoryById",
                id);
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _connectionFactory.ReadListAsync<Category>(
                "sp_GetCategoriesByUserId",
                new { UserId = userId });
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateCategory",
                new
                {
                    category.Id,
                    category.UserId,
                    category.Name,
                    Type = category.Type.ToString(),
                    category.Description,
                    category.CreatedAt
                });
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateCategory",
                new
                {
                    category.Id,
                    category.Name,
                    category.Description,
                    category.UpdatedAt
                });
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteCategory",
                new { category.Id });
        }
    }
}