using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mediating.Sample.Infrastructure.Mediator.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validationFactory;
        private readonly ILogger _logger;

        public ValidationBehavior(IValidatorFactory validationFactory, ILoggerFactory loggingFactory)
        {
            _validationFactory = validationFactory;
            _logger = loggingFactory.CreateLogger("ValidationBehavior");
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validator = _validationFactory.GetValidator(request.GetType());
            var result = validator?.Validate(request);

            if(result != null && !result.IsValid)
                throw new ValidationException(result.Errors);

            var response = await next();
            return response;
        }
    }
}
