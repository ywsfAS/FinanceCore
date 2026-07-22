
using FinanceCore.Application.Abstractions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinanceCore.Infrastructure.Context
{
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

        public async Task BeginAsync(CancellationToken token)
        {
            _connection =  (SqlConnection)_connectionFactory.GetConnection();
            await _connection.OpenAsync(token);
            _transaction = (SqlTransaction)await _connection.BeginTransactionAsync(); 
        }
        public async Task CommitAsync(CancellationToken token)
        {
           if (_transaction is null)
              throw new InvalidOperationException("Cannot commit because no transaction has been started.");
            await _transaction.CommitAsync(token);
        }
        public async Task RollBackAsync(CancellationToken token)
        {
           if (_transaction is null)
              throw new InvalidOperationException("Cannot RollBack because no transaction has been started.");
            await _transaction.RollbackAsync(token);
        }
        public async ValueTask DisposeAsync()
        {

           if (_connection == null || _transaction == null)
              throw new InvalidOperationException("Cannot dispose a non started transaction");

            await _connection.DisposeAsync();
            await _transaction.DisposeAsync();
        }
    }
}
