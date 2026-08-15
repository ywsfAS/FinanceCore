using FinanceCore.Application.Abstractions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinanceCore.Infrastructure.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly IConnectionFactory _connectionFactory;

    private SqlConnection? _connection;
    private SqlTransaction? _transaction;

    public UnitOfWork(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IDbConnection? Connection => _connection;

    public IDbTransaction? Transaction => _transaction;

    public async Task BeginAsync(
        CancellationToken token = default)
    {
        if (_transaction is not null)
            throw new InvalidOperationException(
                "A transaction has already been started.");

        _connection =
            (SqlConnection)_connectionFactory.GetConnection();

        await _connection.OpenAsync(token);

        _transaction =
            (SqlTransaction)await _connection.BeginTransactionAsync(token);
    }

    public async Task CommitAsync(
        CancellationToken token = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException(
                "Cannot commit because no transaction has been started.");

       await _transaction.CommitAsync(token);
    }

    public async Task RollBackAsync(
        CancellationToken token = default)
    {
        if (_transaction is null)
            return;

        await _transaction.RollbackAsync(token);
    }

    private async ValueTask DisposeTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        if (_connection is not null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeTransactionAsync();
    }
}
