using FluentValidation;
using Mediating.Sample.Features.UserManagement;
using Mediating.Sample.Infrastructure.Mediator.PipelineBehaviors;
using MediatR;
using MediatR.Pipeline;
using StackExchange.Redis;
using StructureMap;

namespace Mediating.Sample.Infrastructure.DependencyRegistries
{
    public class MediatorRegistrations : Registry
    {
        public MediatorRegistrations()
        {
            Scan(_ =>
                 {
                     _.TheCallingAssembly();
                     _.WithDefaultConventions();

                     // fluent validation
                     _.AddAllTypesOf(typeof(IValidator<>));
                 });

            // order of the registration matters here
            For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPreProcessorBehavior<,>));
            For(typeof(IPipelineBehavior<,>)).Add(typeof(LoggingBehavior<,>));
            For(typeof(IPipelineBehavior<,>)).Add(typeof(ValidationBehavior<,>));
            For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPostProcessorBehavior<,>));

            For<IUserStorage>().Use<InMemoryUserStorage>().Singleton();
        }
    }
}
