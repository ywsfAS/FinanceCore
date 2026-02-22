using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Categories;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.Models.FinanceCore.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

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
            var model =  await _connectionFactory.ReadSingleAsync<CategoryModel, Guid>(
                "sp_GetCategoryById",
                id);
            if (model == null)
            {
                return null;
            }
            return CategoryMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var models =  await _connectionFactory.ReadListAsync<CategoryModel>(
                "sp_GetCategoriesByUserId",
                new { UserId = userId });
            return models.Select(model => CategoryMapper.MapToDomain(model));
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            var model = CategoryMapper.MapToModel(category);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateCategory",
                model
                );
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var model = CategoryMapper.MapToModel(category);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateCategory",
                model
                );
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteCategory",
                new { category.Id });
        }
    }
}