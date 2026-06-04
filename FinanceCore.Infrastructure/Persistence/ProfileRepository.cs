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
        private async Task<ProfileModel?> GetProfileModelByUserIdAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var parameters = new DynamicParameters();
            var sql = @"SELECT * FROM Profiles WHERE UserId = @UserId";
            parameters.Add("UserId", id);
            var model = await connection.QuerySingleOrDefaultAsync<ProfileModel>(sql,parameters);
            return model;
        }
        public async Task<Profile?> GetProfileByUserIdAsync(Guid userId)
        {
            var profileModel = await GetProfileModelByUserIdAsync(userId);
            if (profileModel is null) return null;
            var profile = ProfileMapper.MapToDomain(profileModel);
            return profile;
        } 
        public async Task<bool> ExistsAsync(Guid id)
        {
            using var connection = _connectionFactory?.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "SELECT 1 FROM Profiles WHERE Id = @Id";
            parameters.Add("Id",id);
            var result = await connection.ExecuteScalarAsync<int>(sql,parameters);
            return result > 0;
        }
        public async Task<bool> ExistsByUserIdAsync(Guid userId)
        {
            using var connection = _connectionFactory?.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "SELECT 1 FROM Profiles WHERE UserId = @Id";
            parameters.Add("Id",userId);
            var result = await connection.ExecuteScalarAsync<int>(sql,parameters);
            return result > 0;
        }
        public async Task<IEnumerable<ProfileModel>> GetAllAsync()
        {
            using var connection = _connectionFactory?.GetConnection();
            var sql = "SELECT * FROM Profiles";
            var result = await connection.QueryAsync<ProfileModel>(sql);
            return result;
        }
        public async Task DeleteAsync(Guid id)
        {
            using var connection = _connectionFactory?.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "DELETE * FROM Profiles WHERE Id = @id";
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
