using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Accounts;
using FinanceCore.Infrastructure.Context;

namespace FinanceCore.Infrastructure.Persistence
{
    public class ReconciliationRepository : IReconciliationRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ReconciliationRepository(IConnectionFactory connectionFactory) { 
            _connectionFactory = connectionFactory;
        }


        public async Task AddAsync(
            Reconciliation reconciliation,
            IUnitOfWork? unitOfWork = null,
            CancellationToken token = default)
        {
            const string sql = """
        INSERT INTO Reconciliations
        (
            Id,
            AccountId,
            ExpectedBalance,
            ActualBalance,
            Reason,
            Notes,
            AdjustmentStatusId,
            AdjustmentTransactionId,
            ReconciledAt,
            CreatedAt
        )
        VALUES
        (
            @Id,
            @AccountId,
            @ExpectedBalance,
            @ActualBalance,
            @Reason,
            @Notes,
            @AdjustmentStatusId,
            @AdjustmentTransactionId,
            @ReconciledAt,
            @CreatedAt
        );
        """;

            var connection = unitOfWork?.Connection ?? _connectionFactory.GetConnection();
            var transaction = unitOfWork?.Transaction;

            var parameters = new
            {
                reconciliation.Id,
                reconciliation.AccountId,
                ExpectedBalance = reconciliation.ExpectedBalance.Amount,
                ActualBalance = reconciliation.ActualBalance.Amount,
                Reason = reconciliation.Reason.ToString(),
                Notes = reconciliation.Notes,
                AdjustmentStatusId = (int)reconciliation.Status,
                reconciliation.AdjustmentTransactionId,
                reconciliation.ReconciledAt,
                reconciliation.CreatedAt
            };

            await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    parameters,
                    transaction,
                    cancellationToken: token));

        }
    }
}
