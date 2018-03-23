using System.Threading;
using System.Threading.Tasks;
using Mediating.Sample.Features.UserManagement.Domain;
using MediatR;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new UserCreatedEmailSent(@event.Username, @event.Email, @event.OccuredAt), cancellationToken);
        }
    }
}
