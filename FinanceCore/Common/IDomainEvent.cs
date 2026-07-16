using MediatR;
namespace FinanceCore.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
