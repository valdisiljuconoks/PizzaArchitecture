namespace Mediating.Sample.Infrastructure.Commands
{
    internal interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>(T command) where T : ICommand;
    }
}
