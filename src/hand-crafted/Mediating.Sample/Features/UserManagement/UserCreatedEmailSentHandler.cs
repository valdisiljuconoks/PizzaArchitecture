using Mediating.Sample.Infrastructure.Events;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserCreatedEmailSentHandler : IDomainEventHandler<UserCreatedEmailSent>
    {
        public void Handle(UserCreatedEmailSent @event)
        {
            // some smanczy/fancy business logic
            // on how to spam users and become even more annoying..
        }
    }
}
