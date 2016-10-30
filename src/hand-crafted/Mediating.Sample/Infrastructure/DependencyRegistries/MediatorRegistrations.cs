using FluentValidation;
using FluentValidation.AspNetCore;
using Mediating.Sample.Features.UserManagement;
using Mediating.Sample.Infrastructure.Commands;
using Mediating.Sample.Infrastructure.Commands.Internal;
using Mediating.Sample.Infrastructure.Events;
using Mediating.Sample.Infrastructure.Events.Internal;
using Mediating.Sample.Infrastructure.Mediator;
using Mediating.Sample.Infrastructure.Mediator.Internal;
using Mediating.Sample.Infrastructure.Queries;
using Mediating.Sample.Infrastructure.Queries.Internal;
using StructureMap;

namespace Mediating.Sample.Infrastructure.DependencyRegistries
{
    public class MediatorRegistrations : Registry
    {
        public MediatorRegistrations()
        {
            Scan(scanner =>
                 {
                     scanner.TheCallingAssembly();
                     scanner.WithDefaultConventions();

                     scanner.AddAllTypesOf(typeof(IValidator<>));

                     scanner.AddAllTypesOf(typeof(ICommandHandler<>));
                     scanner.AddAllTypesOf(typeof(IQueryHandler<,>));
                     scanner.AddAllTypesOf(typeof(IDomainEventHandler<>));

                     scanner.AddAllTypesOf(typeof(IPreExecuteCommandHandler<>));
                 });

            For<IAbstractFactory>().Use<AbstractFactory>().ContainerScoped();
            For<IQueryHandlerFactory>().Use<QueryHandlerFactory>().ContainerScoped();
            For<ICommandHandlerFactory>().Use<CommandHandlerFactory>().ContainerScoped();
            For<IDomainEventHandlerFactory>().Use<DomainEventHandlerFactory>().ContainerScoped();

            For<IValidatorFactory>().Use<ServiceProviderValidatorFactory>().Singleton();

            For(typeof(ICommandHandler<>)).DecorateAllWith(typeof(CommandPipeline<>));

            For<IMediator>().DecorateAllWith<MediatorWithValidation>();
            For<IMediator>().Use<DefaultMediator>().ContainerScoped();

            For<IUserStorage>().Use<InMemoryUserStorage>().Singleton();
        }
    }
}
