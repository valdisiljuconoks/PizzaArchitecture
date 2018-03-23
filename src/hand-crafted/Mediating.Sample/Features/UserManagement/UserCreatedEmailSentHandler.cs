using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserCreatedEmailSentHandler : INotificationHandler<UserCreatedEmailSent>
    {
        public Task Handle(UserCreatedEmailSent notification, CancellationToken cancellationToken)
        {
            // some smanczy/fancy business logic
            // on how to spam users and become even more annoying..

            return Task.CompletedTask;
        }
    }
}
