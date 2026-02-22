using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Accounts;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.Models;


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
            var model = await _connectionFactory.ReadSingleAsync<AccountModel, Guid>(
                "sp_GetAccountById",
                id);
            if (model == null) {
                return null;
            }
            return AccountMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var models =  await _connectionFactory.ReadListAsync<AccountModel>(
                "sp_GetAccountsByUserId",
                new { UserId = userId });
            return models.Select(model => AccountMapper.MapToDomain(model));
        }

        public async Task AddAsync(Account account, CancellationToken token = default)
        {
            var model = AccountMapper.MapToModel(account);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateAccount",
                model
               );
        }

        public async Task UpdateAsync(Account account, CancellationToken token = default)
        {
            var model = AccountMapper.MapToModel(account);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateAccount",
                model
             
                );
        }

        public async Task DeleteAsync(Account account, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteAccount",
                new { account.Id });
        }
    }
}