using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediating.Sample.Infrastructure.Events.Internal
{
    internal class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private readonly IAbstractFactory _abstractFactory;

        public DomainEventHandlerFactory(IAbstractFactory abstractFactory)
        {
            if(abstractFactory == null)
                throw new ArgumentNullException(nameof(abstractFactory));

            _abstractFactory = abstractFactory;
        }

        public IEnumerable<DomainEventHandlerWrapper> GetHandlers<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            /*
             * This whole infrastructure of handler wrappers and such is needed just because I can't ask
             * StructureMap to give me all instances of generic type by providing base class and waiting to return child class implementation.
             * For instance:
             * 
             * MyEvent : IDomainEvent
             * MyHandler : IDomainEventHandler<MyEvent>
             * 
             * and now I'm would be asking StructureMap:
             * 
             * serviceProvider.GetServices<IDomainEventHandler<IDomainEvent>>()
             * 
             * should be expecting to get back also all child class registrations.
             * But this is not how IoC is working in this case. So we need to go via generic types and ask via Type notation, not with type parameters.
             */
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var wrapperType = typeof(DomainEventHandlerWrapper<>).MakeGenericType(@event.GetType());

            var handlerWrappers = _abstractFactory.GetServices(handlerType)
                                                  .Select(handler => Activator.CreateInstance(wrapperType, handler))
                                                  .Cast<DomainEventHandlerWrapper>().ToList();

            return handlerWrappers;
        }
    }
}
