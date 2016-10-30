using System.Linq;

namespace Mediating.Sample.Infrastructure.Commands
{
    public class CommandPipeline<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _inner;
        private readonly IPreExecuteCommandHandler<TCommand>[] _preHandlers;

        public CommandPipeline(ICommandHandler<TCommand> inner, IPreExecuteCommandHandler<TCommand>[] preHandlers)
        {
            _inner = inner;
            _preHandlers = preHandlers;
        }

        public void Handle(TCommand command)
        {
            if(_preHandlers != null && _preHandlers.Any())
            {
                foreach (var handler in _preHandlers)
                {
                    handler.Handle(command);
                }
            }

            _inner.Handle(command);
        }
    }
}
