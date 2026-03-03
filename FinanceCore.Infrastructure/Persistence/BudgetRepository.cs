using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;

namespace FinanceCore.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public BudgetRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> IsExists(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM Budgets WHERE UserId = @Id AND Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
        }
        public async Task<Budget?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model =  await _connectionFactory.ReadSingleAsync<BudgetModel, Guid>(
                "sp_GetBudgetById",
                id);
            if (model == null) {
               return null;
            }
            return BudgetMapper.MapToDomain(model);
        }
        private async Task<BudgetModel?> GetModelByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();

            // Query to get the budget that overlaps the period
            var sql = @"
            SELECT TOP 1 *
            FROM Budgets
            WHERE UserId = @UserId
            AND CategoryId = @CategoryId
            AND StartDate <= @End
            AND EndDate >= @Start
            ORDER BY StartDate DESC";
            // QuerySingleOrDefault maps to BudgetModel
            var budgetModel = await connection.QuerySingleOrDefaultAsync<BudgetModel>(
                sql,
                new
                {
                    UserId = userId,
                    CategoryId = categoryId,
                    Start = start,
                    End = end
                }
            );

            return budgetModel;
        }
        public async Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end)
        {
            var model = await GetModelByCategoryIdAsync(userId, categoryId, start, end);   
            if (model is null)
                return null;

            // Map BudgetModel to BudgetDto
            return new BudgetDto(
                model.Id,
                model.UserId,
                model.CategoryId,
                model.Amount,
                (EnCurrency)model.CurrencyId,
                model.Period,
                model.StartDate,
                model.EndDate
            );
        }

        public async Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {

             var models = await _connectionFactory.ReadListAsync<BudgetModel>(
                "sp_GetBudgetsByUserId",
                new { UserId = userId });
            return models.Select(model => BudgetMapper.MapToDomain(model));
        }
        public async Task<IEnumerable<BudgetDto>?> GetDtoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {

            var models = await _connectionFactory.ReadListAsync<BudgetModel>(
               "sp_GetBudgetsByUserId",
               new { UserId = userId });
            if( models == null ) return null;
            return models.Select(model => new BudgetDto(model.Id,model.UserId,model.CategoryId,model.Amount,(EnCurrency)model.CurrencyId , model.Period,model.StartDate , model.EndDate));

        }

        public async Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            var model = BudgetMapper.MapToModel(budget);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateBudget",
                model
                );
        }

        public async Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            var model = BudgetMapper.MapToModel(budget);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateBudget",
                model
             );
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteBudget",
                new { id});
        }
        public async Task<Budget?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            var model = await GetModelByIdAndUserIdAsync(userId,id,token);
            if(model is null) return null;
            return BudgetMapper.MapToDomain(model);
            
        }
        private async Task<BudgetModel?> GetModelByIdAndUserIdAsync(Guid userId , Guid id , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Budgets WHERE";
            var parameters = new DynamicParameters();
            sql += " Id = @Id";
            parameters.Add("Id", id);
            sql += " AND UserId = @UserId";
            parameters.Add("UserId", userId);

            var model = await connection.QuerySingleOrDefaultAsync<BudgetModel>(sql, parameters);
            return model;

        } 
        public async Task<BudgetDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            var model = await GetModelByIdAndUserIdAsync(userId,id,token);
            if(model is null) return null;
            return new BudgetDto(model.Id, model.UserId, model.CategoryId,model.Amount,(EnCurrency)model.CurrencyId,model.Period , model.StartDate ,model.EndDate);


        }
    }
}