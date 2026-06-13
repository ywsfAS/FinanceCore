using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Data;

namespace FinanceCore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        private async Task<UserModel?> GetModelByIdAsync(Guid id , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"SELECT * FROM Users WHERE Id = @Id";
            var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: token);
            return await connection.QueryFirstOrDefaultAsync<UserModel>(command);
        }
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAsync(id, token);
            if (model is null) {
                return null;
            }
            return UserMapper.MapToDomain(model);
        }
        private async Task<UserModel?> GetModelByEmailAsync(Email email, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"SELECT * FROM Users WHERE Email = @Email";
            var command = new CommandDefinition(sql, new { Email = email.Address }, cancellationToken: token);
            return await connection.QueryFirstOrDefaultAsync<UserModel>(command);
        }
        public async Task<User?> GetByEmailAsync(Email email, CancellationToken token = default)
        {
            var model = await GetModelByEmailAsync(email, token);
            if (model is null)
            {
                return null;
            }
            return UserMapper.MapToDomain(model);
        }
        public async Task AddAsync(User user, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
                INSERT INTO Users (Id, Email, PasswordHash, Name,CreatedAt ,UpdatedAt , TimeZone)
                VALUES (@Id, @Email, @PasswordHash, @Name,@CreatedAt,@UpdatedAt,@TimeZone)";

            var model = UserMapper.MapToModel(user);

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(sql, model, cancellationToken: token, commandType: CommandType.Text));

            if (affectedRows == 0)
                throw new InvalidOperationException("Failed to insert user into the database.");
        }

        public async Task<bool> IsExistsAsync(Guid userId, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"SELECT 1 FROM Users WHERE Id = @userId";

            var result = await connection.ExecuteScalarAsync<int?>(
                new CommandDefinition(sql, new { userId }, cancellationToken: token, commandType: CommandType.Text));

            return result.HasValue;
        }

        public async Task UpdateAsync(User user, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
                UPDATE Users
                SET Email         = @Email,
                    PasswordHash  = @PasswordHash,
                    FirstName     = @FirstName,
                    LastName      = @LastName,
                    UpdatedAt     = @UpdatedAt,
                    TimeZone      = @TimeZone
                WHERE Id = @Id";

            var model = UserMapper.MapToModel(user);

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(sql, model, cancellationToken: token, commandType: CommandType.Text));

            if (affectedRows == 0)
                throw new KeyNotFoundException("User not found.");
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"DELETE FROM Users WHERE Id = @id";

            await connection.ExecuteAsync(
                new CommandDefinition(sql, new { id }, cancellationToken: token, commandType: CommandType.Text));
        }
      }
}