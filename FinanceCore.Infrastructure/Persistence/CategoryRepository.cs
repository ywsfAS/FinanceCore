using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models.FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;

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
        public async Task<bool> IsExists(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM Categories WHERE UserId = @Id AND Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
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

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteCategory",
                new { id });
        }

        public async Task<IEnumerable<CategoryDto>?> GetFiltredCategoriesAsync(Guid? UserId, string? Name , byte? Type ,DateTime? CreatedAt,int Page, int PageSize)
        {
            return await FetchCategoriessPageAsync(UserId, Name, Type, CreatedAt, Page, PageSize);

        }
        public async Task<IEnumerable<CategoryDto>?> GetCategoriesByUserIdAsync(Guid? UserId,int Page, int PageSize,CancellationToken token)
        {
            return await FetchCategoriessPageAsync(UserId,null,null,null, Page, PageSize);

        }
        private async Task<CategoryModel?> GetModelCategoryByIdAndUserIdAsync(Guid userId , Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Categories WHERE ";
            var parameters = new DynamicParameters();
            sql += " AND UserId = @UserId";
            parameters.Add("UserId", userId);
            sql += " AND Id = @Id";
            parameters.Add("Id", id);

            var model = await connection.QuerySingleOrDefaultAsync<CategoryModel>(sql, parameters);
            return model;

        }
        public async Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId , Guid id)
        {
            var model = await  GetModelCategoryByIdAndUserIdAsync(userId,id);
            if(model is null) { return null; }
            return new CategoryDto(model.Id,model.UserId, model.Name,(CategoryType)model.CategoryTypeId,model.Description);
        }

        public async Task<Category?> GetCategoryByIdAndUserIdAsync(Guid userId, Guid id,CancellationToken token)
        {
            var model = await GetModelCategoryByIdAndUserIdAsync(userId, id);
            if (model is null) { return null; }
            return CategoryMapper.MapToDomain(model); 
        }

        private async Task<IEnumerable<CategoryDto>?> FetchCategoriessPageAsync(Guid? UserId, string? Name , byte? Type ,DateTime? CreatedAt,int Page, int PageSize)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Categories WHERE 1 = 1";

            var parameters = new DynamicParameters();
            if (UserId.HasValue)
            {
                sql += " AND UserId = @UserId";
                parameters.Add("UserId", UserId);
            }
            if (CreatedAt.HasValue)
            {
                sql += " AND CreatedAt >= @Start AND CreatedAt <= @End ";
                parameters.Add("Start", CreatedAt);
                parameters.Add("End", CreatedAt.Value.Date.AddDays(1));

            }
            if (Type.HasValue)
            {
                sql += " AND CategoryTypeId = @Type";
                parameters.Add("Type", Type);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                sql += " AND Name = @Name";
                parameters.Add("Name", Name);

            }

            // Order By CreatedAt
            sql += " ORDER BY CreatedAt";
            sql += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";


            parameters.Add("Offset", (Page - 1) * PageSize);
            parameters.Add("PageSize", PageSize);

            var model = await connection.QueryAsync<CategoryModel>(sql, parameters);
            return model.Select(model => new CategoryDto(model.Id,model.UserId, model.Name,(CategoryType)model.CategoryTypeId,model.Description));
        }
    }
}