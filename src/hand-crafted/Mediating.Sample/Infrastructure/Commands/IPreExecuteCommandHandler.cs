namespace Mediating.Sample.Infrastructure.Commands
{
    public interface IPreExecuteCommandHandler<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
