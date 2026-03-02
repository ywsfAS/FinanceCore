using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;

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
            var model = await _connectionFactory.ReadSingleAsync<UserModel, Guid>(
                "sp_GetUserById",
                id);
            if (model == null) {
                return null;
            }
            return UserMapper.MapToDomain(model);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var models = await _connectionFactory.ReadListAsync<UserModel>(
                "sp_GetUserByEmail",
                new { Email = email });
            

            var model = models.FirstOrDefault();
            if (model == null)
            {
                return null;
            }
            return UserMapper.MapToDomain(model);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            var model = UserMapper.MapToModel(user);
                
            await _connectionFactory.ExecuteNonQueryAsync(
             "sp_CreateUser",
             model
             );
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            var model = UserMapper.MapToModel(user);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateUser",
                model
                );
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteUser",
                new { user.Id });
        }
    }
}