using Dapper;
using FinanceCore.Infrastructure.context;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinanceCore.Infrastructure.context.ConnectionFactory
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection() =>
            new SqlConnection(_connectionString);

        public async Task<IEnumerable<T>> ReadListAsync<T>(
            string procedure,
            object? parameters = null)
        {
            using var connection = GetConnection();
            var list = await connection.QueryAsync<T>(
                procedure,
                parameters,
                commandType: CommandType.StoredProcedure);
            return list;
        }

        public async Task<T?> ReadSingleAsync<T, TID>(
            string procedure,
            TID id)
        {
            using var connection = GetConnection();
            var result = await connection.QueryFirstOrDefaultAsync<T>(
                procedure,
                new { Id = id },
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(
            string procedure,
            object? parameters = null)
        {
            using var connection = GetConnection();
            int rows = await connection.ExecuteAsync(
                procedure,
                parameters,
                commandType: CommandType.StoredProcedure);
            return rows;
        }

        public async Task<T?> ExecuteScalarAsync<T>(
            string procedure,
            object? parameters = null)
        {
            using var connection = GetConnection();
            var result = await connection.ExecuteScalarAsync<T>(
                procedure,
                parameters,
                commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}