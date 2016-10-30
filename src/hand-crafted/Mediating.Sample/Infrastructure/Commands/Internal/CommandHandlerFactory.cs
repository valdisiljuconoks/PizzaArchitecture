using System;

namespace Mediating.Sample.Infrastructure.Commands.Internal
{
    internal class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IAbstractFactory _abstractFactory;

        public CommandHandlerFactory(IAbstractFactory abstractFactory)
        {
            if(abstractFactory == null)
                throw new ArgumentNullException(nameof(abstractFactory));

            _abstractFactory = abstractFactory;
        }

        public ICommandHandler<T> GetHandler<T>(T command) where T : ICommand
        {
            return _abstractFactory.GetService<ICommandHandler<T>>();
        }
    }
}
