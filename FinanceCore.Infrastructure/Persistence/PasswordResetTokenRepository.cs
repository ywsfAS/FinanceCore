using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.PasswordRestToken;
using FinanceCore.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Persistence
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public PasswordResetTokenRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(PasswordResetToken token,CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
            INSERT INTO PasswordResetTokens (
                Id,
                UserId,
                Token,
                ExpiresAt,
                IsUsed,
                CreatedAt
             )
             VALUES (
                @Id,
                @UserId,
                @Token,
                @ExpiresAt,
                @IsUsed,
                @CreatedAt
             )

            ";

            var command = new CommandDefinition(sql, new { Id = token.Id, UserId = token.UserId , token = token.Token , ExpiresAt = token.ExpiresAt , IsUsed = token.IsUsed , CreatedAt = token.CreatedAt } , cancellationToken : cancellationToken);
            await connection.ExecuteAsync(command);

        }
        public async Task<PasswordResetToken?> GetByTokenAsync(string token,CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT *
                FROM PasswordResetTokens
                WHERE Token = @Token";

            return await connection.QueryFirstOrDefaultAsync<PasswordResetToken>(
                new CommandDefinition(
                    sql,
                    new { Token = token },
                    cancellationToken: cancellationToken));
        }
        public async Task MarkAsUsedAsync(PasswordResetToken token,CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
                UPDATE PasswordResetTokens
                SET IsUsed = 1
                WHERE Id = @Id
            ";
            var command = new CommandDefinition(sql, new {Id = token.Id} , cancellationToken: cancellationToken);
            await connection.ExecuteAsync(command);
        }
    }
}
