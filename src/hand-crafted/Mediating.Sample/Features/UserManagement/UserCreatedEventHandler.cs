using Mediating.Sample.Features.UserManagement.Domain;
using Mediating.Sample.Infrastructure.Events;
using Mediating.Sample.Infrastructure.Mediator;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Handle(UserCreatedEvent @event)
        {
            // some smanczy/fancy business logic
            // on how to spam users and become even more annoying..

            _mediator.Publish(new UserCreatedEmailSent(@event.Username, @event.Email, @event.OccuredAt));
        }
    }
}