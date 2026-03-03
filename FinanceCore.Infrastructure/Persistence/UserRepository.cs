using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;

namespace FinanceCore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        private async Task<UserModel?> GetModelByIdAsync(Guid id , CancellationToken token
        )
        {
            var model = await _connectionFactory.ReadSingleAsync<UserModel, Guid>(
                "sp_GetUserById",
                id);
            return model;
        }
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await GetModelByIdAsync(id, cancellationToken);
            if (model is null) {
                return null;
            }
            return UserMapper.MapToDomain(model);
        }
        private async Task<UserModel?> GetModelByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var models = await _connectionFactory.ReadListAsync<UserModel>(
                "sp_GetUserByEmail",
                new { Email = email });
            return models.FirstOrDefault();
        }
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var model = await GetModelByEmailAsync(email, cancellationToken);
            if (model is null)
            {
                return null;
            }
            return UserMapper.MapToDomain(model);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            var model = UserMapper.MapToModel(user);
                
            await _connectionFactory.ExecuteNonQueryAsync(
             "sp_CreateUser",
             model
             );
        }
        public async Task<bool> IsExists(Guid userId,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM Accounts WHERE UserId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
        }
        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            var model = UserMapper.MapToModel(user);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateUser",
                model
                );
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteUser",
                new { id });
        }
    }
}