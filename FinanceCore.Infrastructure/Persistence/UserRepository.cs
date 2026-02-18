using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;

namespace FinanceCore.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _connectionFactory.ReadSingleAsync<User, Guid>(
                "sp_GetUserById",
                id);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var users = await _connectionFactory.ReadListAsync<User>(
                "sp_GetUserByEmail",
                new { Email = email });

            return users.FirstOrDefault();
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateUser",
                new
                {
                    user.Id,
                    user.Name,
                    Email = user.Email.Address,
                    user.PasswordHash,
                    DefaultCurrency = user.DefaultCurrency.ToString(),
                    user.TimeZone,
                    user.CreatedAt
                });
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateUser",
                new
                {
                    user.Id,
                    user.Name,
                    DefaultCurrency = user.DefaultCurrency.ToString(),
                    user.TimeZone,
                    user.UpdatedAt
                });
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteUser",
                new { user.Id });
        }
    }
}