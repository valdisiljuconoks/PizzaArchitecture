using Mediating.Sample.Infrastructure.Commands;
using Mediating.Sample.Infrastructure.Events;
using Mediating.Sample.Infrastructure.Queries;
using Microsoft.Extensions.Logging;

namespace Mediating.Sample.Infrastructure.Mediator.Internal
{
    internal class LoggingMediator : IMediator
    {
        private readonly IMediator _inner;
        private readonly ILogger _logger;

        public LoggingMediator(IMediator inner, ILoggerFactory factory)
        {
            _inner = inner;
            _logger = factory.CreateLogger<LoggingMediator>();
        }

        public void Execute<T>(T command) where T : ICommand
        {
            _logger.LogInformation($"Executing `{command.GetType().FullName}` command...");
            _inner.Execute(command);
            _logger.LogInformation($"Finished `{command.GetType().FullName}`.");
        }

        public void Publish<T>(T @event) where T : IDomainEvent
        {
            _inner.Publish(@event);
            _logger.LogInformation($"Published `{@event.GetType().FullName}` event.");
        }

        public TResult Query<TResult>(IQuery<TResult> query)
        {
            _logger.LogInformation($"Executing `{query.GetType().FullName}` query...");
            var result = _inner.Query(query);
            _logger.LogInformation($"Executed `{query.GetType().FullName}` query.");
            return result;
        }
    }
}
