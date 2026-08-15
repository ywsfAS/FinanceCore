using Dapper;
using FinanceCore.Accounts;
using FinanceCore.Infrastructure.Context;
using FinanceCore.Application.Abstractions;

namespace FinanceCore.Infrastructure.Persistence;

public class AlertRepository : IAlertRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public AlertRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyCollection<LowBalanceAlert>> GetActiveAlertsAsync(
        Guid accountId,
        CancellationToken token)
    {
        const string sql = """
            SELECT
                Id,
                AccountId,
                ThresholdAmount,
                IsEnabled,
                LastTriggeredAt,
                CreatedAt,
                UpdatedAt
            FROM Alerts
            WHERE AccountId = @AccountId
              AND IsEnabled = 1;
            """;

        using var connection = _connectionFactory.GetConnection();

        var command = new CommandDefinition(
            sql,
            new { AccountId = accountId },
            cancellationToken: token);

        var alerts = await connection.QueryAsync<LowBalanceAlert>(command);

        return alerts.ToArray();
    }

    public async Task CreateAsync(
        LowBalanceAlert alert,
        CancellationToken token)
    {
        const string sql = """
            INSERT INTO Alerts
            (
                Id,
                AccountId,
                ThresholdAmount,
                IsEnabled,
                LastTriggeredAt,
                CreatedAt,
                UpdatedAt
            )
            VALUES
            (
                @Id,
                @AccountId,
                @ThresholdAmount,
                @IsEnabled,
                @LastTriggeredAt,
                @CreatedAt,
                @UpdatedAt
            );
            """;

        using var connection = _connectionFactory.GetConnection();

        var command = new CommandDefinition(
            sql,
            alert,
            cancellationToken: token);

        await connection.ExecuteAsync(command);
    }

    public async Task UpdateAsync(
        LowBalanceAlert alert,
        CancellationToken token)
    {
        const string sql = """
            UPDATE Alerts
            SET
                ThresholdAmount = @ThresholdAmount,
                IsEnabled = @IsEnabled,
                LastTriggeredAt = @LastTriggeredAt,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;
            """;

        using var connection = _connectionFactory.GetConnection();

        var command = new CommandDefinition(
            sql,
            alert,
            cancellationToken: token);

        await connection.ExecuteAsync(command);
    }
}
