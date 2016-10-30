using FluentValidation;
using Mediating.Sample.Infrastructure.Commands;
using Mediating.Sample.Infrastructure.Events;
using Mediating.Sample.Infrastructure.Queries;

namespace Mediating.Sample.Infrastructure.Mediator.Internal
{
    internal class MediatorWithValidation : IMediator
    {
        private readonly IValidatorFactory _factory;
        private readonly IMediator _inner;

        public MediatorWithValidation(IMediator inner, IValidatorFactory factory)
        {
            _inner = inner;
            _factory = factory;
        }

        public TResult Query<TResult>(IQuery<TResult> query)
        {
            var validator = _factory.GetValidator(query.GetType());
            var result = validator?.Validate(query);

            if((result != null) && !result.IsValid)
                throw new ValidationException(result.Errors);

            return _inner.Query(query);
        }

        public void Execute<T>(T command) where T : ICommand
        {
            var validator = _factory.GetValidator(command.GetType());
            var result = validator?.Validate(command);

            if((result != null) && !result.IsValid)
                throw new ValidationException(result.Errors);

            _inner.Execute(command);
        }

        public void Publish<T>(T @event) where T : IDomainEvent
        {
            _inner.Publish(@event);
        }
    }
}
