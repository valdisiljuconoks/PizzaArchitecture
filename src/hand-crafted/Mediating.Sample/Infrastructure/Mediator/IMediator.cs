using Mediating.Sample.Infrastructure.Commands;
using Mediating.Sample.Infrastructure.Events;
using Mediating.Sample.Infrastructure.Queries;

namespace Mediating.Sample.Infrastructure.Mediator
{
    public interface IMediator
    {
        void Execute<T>(T command) where T : ICommand;

        void Publish<T>(T @event) where T : IDomainEvent;

        TResult Query<TResult>(IQuery<TResult> query);
    }
}
