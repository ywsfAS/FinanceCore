using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.context
{

    public interface IConnectionFactory
    {
        /// <summary>
        /// Returns a new SQL connection.
        /// </summary>
        IDbConnection GetConnection();

        /// <summary>
        /// Executes a stored procedure and maps each row to an entity of type T.
        /// </summary>
        Task<IEnumerable<T>> ReadListAsync<T>(string procedure, object? parameters = null);

        /// <summary>
        /// Executes a stored procedure and maps the first row to a single entity.
        /// </summary>
        Task<T?> ReadSingleAsync<T, TID>(string procedure, TID id);

        /// <summary>
        /// Executes a stored procedure for insert/update/delete operations.
        /// </summary>
        Task<int> ExecuteNonQueryAsync(string procedure, object? parameters = null);

        /// <summary>
        /// Executes a stored procedure and returns a scalar value.
        /// </summary>
        Task<T?> ExecuteScalarAsync<T>(string procedure, object? parameters = null);
        Task<T> QuerySingleAsync<T>(string procedure , object? parameters = null);

    }

}
