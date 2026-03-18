using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Profile;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Data;
using System.Formats.Tar;

namespace FinanceCore.Infrastructure.Persistence
{
    public  class ProfileRepository : IProfileRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProfileRepository(IConnectionFactory connectionFactory) { 
            _connectionFactory = connectionFactory;
        }   
        public async Task<ProfileModel?> GetProfileByUserIdAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "SELECT * FROM Profile WHERE UserId = @UserId";
            parameters.Add("UserId", id);
            var model = await connection.QuerySingleOrDefaultAsync<ProfileModel>(sql,parameters);
            return model;
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            using var connection = _connectionFactory?.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "SELECT 1 FROM Profile WHERE Id = @Id";
            parameters.Add("Id",id);
            var result = await connection.ExecuteScalarAsync<int>(sql,parameters);
            return result > 0;
        }
        public async Task<IEnumerable<ProfileModel>> GetAllAsync()
        {
            using var connection = _connectionFactory?.GetConnection();
            var sql = "SELECT * FROM Profile";
            var result = await connection.QueryAsync<ProfileModel>(sql);
            return result;
        }
        public async Task DeleteAsync(Guid id)
        {
            using var connection = _connectionFactory?.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "DELETE * FROM Profile WHERE Id = @id";
            parameters.Add("id",id);
            var result = await connection.ExecuteAsync(sql,parameters);
          
        }
        public async Task AddAsync(Profile profile, CancellationToken cancellationToken = default)
        {
            const string sql = @"
        INSERT INTO Profiles (
            UserId,
            FirstName,
            LastName,
            Bio,
            AvatarUrl,
            Currency
        )
        VALUES (
            @UserId,
            @FirstName,
            @LastName,
            @Bio,
            @AvatarUrl,
            @Currency
        );";

            var model = ProfileMapper.MapToModel(profile);

            using var connection = _connectionFactory.GetConnection();

            await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    model,
                    cancellationToken: cancellationToken,
                    commandType: CommandType.Text));
        }
        public async Task UpdateAsync(Profile profile, CancellationToken cancellationToken = default)
        {
            const string sql = @"
        UPDATE Profiles
        SET
            FirstName = @FirstName,
            LastName = @LastName,
            Bio = @Bio,
            AvatarUrl = @AvatarUrl,
            Currency = @Currency
        WHERE UserId = @UserId;";

            var model = ProfileMapper.MapToModel(profile);

            using var connection = _connectionFactory.GetConnection();

            await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    model,
                    cancellationToken: cancellationToken,
                    commandType: CommandType.Text));
        }
    }
}
