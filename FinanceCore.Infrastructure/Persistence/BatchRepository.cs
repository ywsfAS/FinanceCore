using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Batch;
using FinanceCore.Infrastructure.Context;

namespace FinanceCore.Infrastructure.Persistence
{
    public class BatchRepository : IBatchRepository
    {

        private readonly IConnectionFactory _connectionFactory;
        public BatchRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(
        Batch batch,
        IUnitOfWork? unitOfWork = null,
        CancellationToken token = default)
        {
            const string sql = """
        INSERT INTO Batches
        (
            Id,
            AccountId,
            FileName,
            ImportedAt,
            TransactionCount
        )
        VALUES
        (
            @Id,
            @AccountId,
            @FileName,
            @ImportedAt,
            @TransactionCount
        );
        """;

            if (unitOfWork is not null)
            {
                var cmd = new CommandDefinition(
                    sql,
                    batch,
                    transaction: unitOfWork.Transaction,
                    cancellationToken: token);

                await unitOfWork.Connection.ExecuteAsync(cmd);

                return;
            }

            using var connection = _connectionFactory.GetConnection();

            var command = new CommandDefinition(
                sql,
                batch,
                cancellationToken: token);

            await connection.ExecuteAsync(command);
        }
    }
}
