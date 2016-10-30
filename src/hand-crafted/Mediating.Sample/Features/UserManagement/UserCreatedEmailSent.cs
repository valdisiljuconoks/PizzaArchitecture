using System;
using Mediating.Sample.Infrastructure.Events;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserCreatedEmailSent : IDomainEvent
    {
        public UserCreatedEmailSent(string username, string email, DateTime when)
        {
            if(string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if(string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            Username = username;
            Email = email;
            WhenSent = when;
        }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public DateTime WhenSent { get; private set; }

        public DateTime OccuredAt { get; } = DateTime.UtcNow;
    }
}
