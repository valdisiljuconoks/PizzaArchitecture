using System;
using Mediating.Sample.Infrastructure.Commands;
using Mediating.Sample.Infrastructure.Events;
using Mediating.Sample.Infrastructure.Queries;

namespace Mediating.Sample.Infrastructure.Mediator
{
    internal class DefaultMediator : IMediator
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IDomainEventHandlerFactory _eventHandlerFactory;
        private readonly IQueryHandlerFactory _queryHandlerFactory;

        public DefaultMediator(IQueryHandlerFactory queryHandlerFactory,
                               ICommandHandlerFactory commandHandlerFactory,
                               IDomainEventHandlerFactory eventHandlerFactory)
        {
            if(queryHandlerFactory == null)
                throw new ArgumentNullException(nameof(queryHandlerFactory));

            if(commandHandlerFactory == null)
                throw new ArgumentNullException(nameof(commandHandlerFactory));

            if(eventHandlerFactory == null)
                throw new ArgumentNullException(nameof(eventHandlerFactory));

            _queryHandlerFactory = queryHandlerFactory;
            _commandHandlerFactory = commandHandlerFactory;
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Execute<T>(T command) where T : ICommand
        {
            var handler = _commandHandlerFactory.GetHandler(command);
            if(handler == null)
                throw new InvalidOperationException($"Command `{command.GetType().FullName}` handler not found.");

            handler.Handle(command);
        }

        public void Publish<T>(T @event) where T : IDomainEvent
        {
            var handlers = _eventHandlerFactory.GetHandlers(@event);
            if(handlers == null)
                return;

            foreach (var handler in handlers)
                handler.Handle(@event);
        }

        public TResult Query<TResult>(IQuery<TResult> query)
        {
            var handler = _queryHandlerFactory.GetHandler(query);
            if(handler == null)
                throw new InvalidOperationException($"Query `{query.GetType().FullName}` handler not found.");

            return handler.Handle(query);
        }
    }
}
