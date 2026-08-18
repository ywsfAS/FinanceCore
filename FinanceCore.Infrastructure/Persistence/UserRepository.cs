using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.Context;
using FinanceCore.Infrastructure.Mappers;
using System.Data;
using System.Text;

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

        public async Task UpdateLoginSecurityStateAsync(Guid userId, int failedLoginAttempts, DateTime? lockedUntil,CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = """
            UPDATE Users
            SET FailedLoginAttempts = @FailedLoginAttempts,
            LockedUntil = @LockedUntil,
            UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @UserId;
            """;
            var command = new CommandDefinition(sql, new {UserId = userId , FailedLoginAttempts = failedLoginAttempts , LockedUntil = lockedUntil} , cancellationToken : token);

            await connection.ExecuteAsync(command);
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
                    Role          = @Role,
                    FailedLoginAttempts = @FailedLoginAttempts,
                    LockedUntil  = @LockedUntil,
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
        public async Task<PagedResult<UserDto>> GetUsersAsync(
        string? search,
        string? role,
        bool? isLocked,
        int page,
        int pageSize, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();

            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var offset = (page - 1) * pageSize;

            const string sql = """
        SELECT
            Id,
            Name,
            Email,
            TimeZone
        FROM Users
        WHERE
            (
                @Search IS NULL
                OR Name LIKE '%' + @Search + '%'
                OR Email LIKE '%' + @Search + '%'
            )
            AND
            (
                @Role IS NULL
                OR Role = @Role
            )
            AND
            (
                @IsLocked IS NULL
                OR (@IsLocked = 1 AND LockedUntil IS NOT NULL AND LockedUntil > SYSUTCDATETIME())
                OR (@IsLocked = 0 AND (LockedUntil IS NULL OR LockedUntil <= SYSUTCDATETIME()))
            )
        ORDER BY CreatedAt DESC
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*)
        FROM Users
        WHERE
            (
                @Search IS NULL
                OR Name LIKE '%' + @Search + '%'
                OR Email LIKE '%' + @Search + '%'
            )
            AND
            (
                @Role IS NULL
                OR Role = @Role
            )
            AND
            (
                @IsLocked IS NULL
                OR (@IsLocked = 1 AND LockedUntil IS NOT NULL AND LockedUntil > SYSUTCDATETIME())
                OR (@IsLocked = 0 AND (LockedUntil IS NULL OR LockedUntil <= SYSUTCDATETIME()))
            );
        """;

            var command = new CommandDefinition(
                sql,
                new
                {
                    Search = string.IsNullOrWhiteSpace(search)
                        ? null
                        : search.Trim(),

                    Role = string.IsNullOrWhiteSpace(role)
                        ? null
                        : role.Trim(),

                    IsLocked = isLocked,
                    Offset = offset,
                    PageSize = pageSize
                },
                cancellationToken: token);

            using var multi = await connection.QueryMultipleAsync(command);

            var users = await multi.ReadAsync<UserDto>();
            var totalCount = await multi.ReadSingleAsync<int>();

            return new PagedResult<UserDto>(
                users,
                totalCount,
                page,
                pageSize);
        }
    }
}