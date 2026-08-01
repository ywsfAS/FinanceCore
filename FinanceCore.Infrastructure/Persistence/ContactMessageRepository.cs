using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.ContactMessage;
using FinanceCore.Infrastructure.Context;
using FinanceCore.Infrastructure.Mappers;

namespace FinanceCore.Infrastructure.Persistence
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ContactMessageRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(ContactMessage msg , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
                INSERT INTO ContactMessages(
                    Id,
                    FullName,
                    Email,
                    Message,
                    SubjectId,
                    IsProccessed,
                    CreatedAt 
                 )
                 VALUES(
                    @Id,
                    @FullName,
                    @Email,
                    @Message,
                    @SubjectId,
                    @IsProccessed,
                    @CreatedAt
                 )
            ";
            var command = new CommandDefinition(sql, new {Id = msg.Id , FullName = msg.FullName ,Email = msg.Email.Address , Message = msg.Message , SubjectId = (byte)msg.Subject , CreatedAt = msg.CreatedAt , isProccessed = msg.IsProccessed } , cancellationToken : token);
            await connection.ExecuteAsync(command);
        }
        public async Task MarkAsSeen(ContactMessage msg , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"UPDATE ContactMessages SET IsProccessed = 1 WHERE Id = @Id";
            var command = new CommandDefinition(sql , new { Id = msg.Id },cancellationToken : token);

            var afftectedRows = await connection.ExecuteAsync(command);
            if(afftectedRows == 0)
            {
                throw new InvalidOperationException(
                $"Contact message with ID {msg.Id} was not found.");
            }

        }
        private async Task<ContactMessageModel?> FindAsync(Guid msgId , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"SELECT * FROM ContactMessages WHERE Id = @Id";
            var query = new CommandDefinition(sql , new { Id = msgId},cancellationToken : token);
            var msg = await connection.QueryFirstOrDefaultAsync<ContactMessageModel>(query);
            return msg;
        }
        public async Task<ContactMessage?> GetContactMessageAsync(Guid msgId,CancellationToken token)
        {
            var msgModel = await FindAsync(msgId,token);
            if (msgModel is null) return null;
            return ContactMessageMapper.MapToDomain(msgModel);
        }

          
            
    }
}
