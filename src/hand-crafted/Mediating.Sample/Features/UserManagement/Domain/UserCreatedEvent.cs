using System;
using Mediating.Sample.Infrastructure.Events;

namespace Mediating.Sample.Features.UserManagement.Domain
{
    public class UserCreatedEvent : IDomainEvent
    {
        public UserCreatedEvent(string username, string email)
        {
            if(string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if(string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            Username = username;
            Email = email;
            OccuredAt = DateTime.UtcNow;
        }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public DateTime OccuredAt { get; }
    }
}
