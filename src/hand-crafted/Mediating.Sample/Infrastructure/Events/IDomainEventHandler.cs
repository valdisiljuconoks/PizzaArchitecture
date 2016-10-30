namespace Mediating.Sample.Infrastructure.Events
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }
}
