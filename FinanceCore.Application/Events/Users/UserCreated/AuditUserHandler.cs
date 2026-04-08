using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Audit;
using FinanceCore.Domain.Events.User;
using MediatR;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class AuditUserHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IAuditRepository _auditRepository;
        public AuditUserHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var audit = new AuditLog("User Created",notification.Name, notification.UserId, DateTime.UtcNow
            );
            await _auditRepository.LogAsync(audit, cancellationToken);
        }
        
    }
}
