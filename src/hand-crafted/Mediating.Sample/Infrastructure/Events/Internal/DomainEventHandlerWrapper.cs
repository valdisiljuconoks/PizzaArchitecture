namespace Mediating.Sample.Infrastructure.Events.Internal
{
    internal abstract class DomainEventHandlerWrapper
    {
        public abstract void Handle(IDomainEvent args);
    }

    internal class DomainEventHandlerWrapper<T> : DomainEventHandlerWrapper where T : IDomainEvent
    {
        /*
         * NOTE: this non-generic guy is needed because at the moment when event dispatch takes place
         * we are dealing with IDomainEvent interface which is generic type argument for domain event handlers
         * like:
         * 
         * MyHandler : DomainEventHandler<MyEvent>
         * MyEvent : IDomainEvent
         * 
         * IoC container gets MyHandler : DomainEventHandler<MyEvent> as plug-in family
         * so at that moment when we are dispatching events, we can't cast from DomainEventHandler<IDomainEvent> to DomainEventHandler<MyEvent>
         * and then call target handle method with typed in argument.
         * 
         * Generic type arguments are not covariant!
         * 
         * For this reason - one of the workaround is to have non-generic interface, which calls child implementation with correct type
         * 
         */

        private readonly IDomainEventHandler<T> _inner;

        public DomainEventHandlerWrapper(IDomainEventHandler<T> inner)
        {
            _inner = inner;
        }

        public override void Handle(IDomainEvent args)
        {
            _inner.Handle((T)args);
        }
    }
}
