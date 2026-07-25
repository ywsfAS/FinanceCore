using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.RefreshToken;
using FinanceCore.Infrastructure.Context;

namespace FinanceCore.Infrastructure.Persistence;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public RefreshTokenRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<RefreshToken?> GetByTokenHashAsync(
        string token,
        CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.GetConnection();

        const string sql = """
            SELECT
                Id,
                UserId,
                TokenHash,
                ExpiresAt,
                RevokedAt,
                DeviceLabel,
                UserAgent,
                LastUsedAt,
                CreatedAt
            FROM RefreshTokens
            WHERE TokenHash = @TokenHash;
            """;

        return await connection.QuerySingleOrDefaultAsync<RefreshToken>(
            new CommandDefinition(
                sql,
                new
                {
                    TokenHash = token
                },
                cancellationToken: cancellationToken));
    }

    public async Task AddAsync(
        RefreshToken token,
        CancellationToken cancellationToken, IUnitOfWork? unitOfWork)
    {

        const string sql = """
            INSERT INTO RefreshTokens
            (
                Id,
                UserId,
                TokenHash,
                ExpiresAt,
                RevokedAt,
                DeviceLabel,
                UserAgent,
                LastUsedAt,
                CreatedAt
            )
            VALUES
            (
                @Id,
                @UserId,
                @TokenHash,
                @ExpiresAt,
                @RevokedAt,
                @DeviceLabel,
                @UserAgent,
                @LastUsedAt,
                @CreatedAt
            );
            """;
        var command = new CommandDefinition(
                 sql,
                 token,
                 transaction : unitOfWork?.Transaction,
                 cancellationToken: cancellationToken);

        if (unitOfWork is null) { 
            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(command);
            return;
        }
        await unitOfWork.Connection.ExecuteAsync(command);
    }

    public async Task RevokeRefreshTokenAsync(
        Guid refreshTokenId,
        DateTime revokedAt,
        CancellationToken cancellationToken,IUnitOfWork? unitOfWork)
    {

        const string sql = """
            UPDATE RefreshTokens
            SET RevokedAt = @RevokedAt
            WHERE Id = @RefreshTokenId
              AND RevokedAt IS NULL;
            """;

        var command = new CommandDefinition(
                 sql,
                 new
                {
                    RefreshTokenId = refreshTokenId,
                    RevokedAt = revokedAt
                },
                 transaction : unitOfWork?.Transaction,
                 cancellationToken: cancellationToken);

        if (unitOfWork is null) { 
            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(command);
            return;
        }
        await unitOfWork.Connection.ExecuteAsync(command);
    }

    public async Task RevokeAllForUserAsync(
        Guid userId,
        DateTime revokedAt,
        CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.GetConnection();

        const string sql = """
            UPDATE RefreshTokens
            SET RevokedAt = @RevokedAt
            WHERE UserId = @UserId
              AND RevokedAt IS NULL;
            """;

        await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    UserId = userId,
                    RevokedAt = revokedAt
                },
                cancellationToken: cancellationToken));
    }
}