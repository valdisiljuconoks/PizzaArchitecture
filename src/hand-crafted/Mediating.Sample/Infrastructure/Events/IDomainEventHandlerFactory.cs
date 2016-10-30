using System.Collections.Generic;
using Mediating.Sample.Infrastructure.Events.Internal;

namespace Mediating.Sample.Infrastructure.Events
{
    internal interface IDomainEventHandlerFactory
    {
        IEnumerable<DomainEventHandlerWrapper> GetHandlers<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}
