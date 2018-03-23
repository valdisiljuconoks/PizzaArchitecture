using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mediating.Sample.Infrastructure.Mediator.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("LoggingBehavior");
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Executing `{request.GetType().FullName}` request...");
            var response = await next();
            _logger.LogInformation($"Finished `{request.GetType().FullName}`.");

            return response;
        }
    }
}
