using FinanceCore.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Events
{
    public static class DomainEventDispatcher
    {
        public static async Task DispatchAsync(IMediator mediator , Entity entity, CancellationToken token)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await mediator.Publish(domainEvent, token);
            }

            entity.ClearDomainEvents();
        }
    }
}
