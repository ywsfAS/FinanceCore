using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Accounts;
using FinanceCore.Infrastructure.context;


namespace FinanceCore.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AccountRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _connectionFactory.ReadSingleAsync<Account, Guid>(
                "sp_GetAccountById",
                id);
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            return await _connectionFactory.ReadListAsync<Account>(
                "sp_GetAccountsByUserId",
                new { UserId = userId });
        }

        public async Task AddAsync(Account account, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateAccount",
                new
                {
                    account.Id,
                    account.UserId,
                    account.Name,
                    Type = account.Type.ToString(),
                    Balance = account.Balance.Amount,
                    Currency = account.Balance.Currency.ToString(),
                    account.CreatedAt
                });
        }

        public async Task UpdateAsync(Account account, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateAccount",
                new
                {
                    account.Id,
                    account.Name,
                    Type = account.Type.ToString(),
                    Balance = account.Balance.Amount,
                    Currency = account.Balance.Currency.ToString(),
                  
                });
        }

        public async Task DeleteAsync(Account account, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteAccount",
                new { account.Id });
        }
    }
}