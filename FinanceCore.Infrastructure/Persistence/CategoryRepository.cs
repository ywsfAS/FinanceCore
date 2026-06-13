using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models.FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Data;

namespace FinanceCore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CategoryRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM Categories WHERE UserId = @UserId AND Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
        }
        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            const string sql = @"
        INSERT INTO Categories (
            Id,
            UserId,
            Name,
            CategoryTypeId,
            Description,
            IsActive,
            IsDefault,
            CreatedAt,
            UpdatedAt
        )
        VALUES (
            @Id,
            @UserId,
            @Name,
            @CategoryTypeId,
            @Description,
            @IsActive,
            @IsDefault,
            @CreatedAt,
            @UpdatedAt
        );";

            var model = CategoryMapper.MapToModel(category);

            using var connection = _connectionFactory.GetConnection();

            await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    model,
                    cancellationToken: cancellationToken,
                    commandType: CommandType.Text));
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            const string sql = @"
        UPDATE Categories
        SET Name = @Name,
            CategoryTypeId = @CategoryTypeId,
            Description = @Description,
            IsActive = @IsActive,
            IsDefault = @IsDefault,
            UpdatedAt = @UpdatedAt
        WHERE Id = @Id
          AND UserId = @UserId";

            var model = CategoryMapper.MapToModel(category);

            using var connection = _connectionFactory.GetConnection();

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    model,
                    cancellationToken: cancellationToken,
                    commandType: CommandType.Text));

            if (affectedRows == 0)
                throw new Exception("Category not found.");
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteCategory",
                new { id });
        }

        public async Task<IEnumerable<CategoryDto>> GetFiltredCategoriesAsync(Guid UserId, string? Name , CategoryType? Type ,DateTime? CreatedAt,int Page, int PageSize,CancellationToken token = default)
        {
            return await FetchCategoriesPageAsync(UserId, Name, Type, CreatedAt, Page, PageSize);

        }
        private async Task<CategoryModel?> GetModelCategoryByIdAndUserIdAsync(Guid userId , Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Categories WHERE ";
            var parameters = new DynamicParameters();
            sql += "UserId = @UserId";
            parameters.Add("UserId", userId);
            sql += " AND Id = @Id";
            parameters.Add("Id", id);

            var model = await connection.QuerySingleOrDefaultAsync<CategoryModel>(sql, parameters);
            return model;

        }
        public async Task<CategoryDto?> GetDtoCategoryByIdAndUserIdAsync(Guid userId , Guid id , CancellationToken token = default)
        {
            var model = await  GetModelCategoryByIdAndUserIdAsync(userId,id);
            if(model is null) { return null; }
            return new CategoryDto(model.Id,model.UserId, model.Name,(CategoryType)model.CategoryTypeId,model.Description);
        }

        public async Task<Category?> GetCategoryByIdAndUserIdAsync(Guid userId, Guid id,CancellationToken token = default)
        {
            var model = await GetModelCategoryByIdAndUserIdAsync(userId, id);
            if (model is null) { return null; }
            return CategoryMapper.MapToDomain(model); 
        }

        private async Task<IEnumerable<CategoryDto>> FetchCategoriesPageAsync(Guid userId, string? name ,CategoryType? type ,DateTime? createdAt,int page, int pageSize)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Categories WHERE UserId = @UserId";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            if (createdAt.HasValue)
            {
                sql += " AND CreatedAt >= @Start AND CreatedAt <= @End ";
                parameters.Add("Start", createdAt);
                parameters.Add("End", createdAt.Value.Date.AddDays(1));

            }
            if (type.HasValue)
            {
                sql += " AND CategoryTypeId = @Type";
                parameters.Add("Type", type);
            }
            if (!string.IsNullOrEmpty(name))
            {
                sql += " AND Name LIKE @Name";
                parameters.Add("Name", $"%{name}%");

            }

            // Order By CreatedAt
            sql += " ORDER BY CreatedAt";
            sql += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";


            parameters.Add("Offset", (page - 1) * pageSize);
            parameters.Add("PageSize", pageSize);


            var model = await connection.QueryAsync<CategoryModel>(sql, parameters);
            return model.Select(model => new CategoryDto(model.Id,model.UserId, model.Name,(CategoryType)model.CategoryTypeId,model.Description));
        }

        public async Task<IEnumerable<CategoryOptionDto>?> GetCategoriesByUserOptionsAsync(Guid userId , int page , int pageSize , CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @" SELECT Id , Name FROM Categories WHERE UserId = @Id ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var command = new CommandDefinition(sql, new { Id = userId, Offset = (page - 1) * pageSize , PageSize = pageSize }, cancellationToken: token);
            return await connection.QueryAsync<CategoryOptionDto>(command);

        }
    }
}