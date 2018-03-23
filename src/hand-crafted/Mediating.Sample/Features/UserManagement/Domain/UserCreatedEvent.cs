using System;
using MediatR;

namespace Mediating.Sample.Features.UserManagement.Domain
{
    public class UserCreatedEvent : INotification
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

        public string Username { get; }

        public string Email { get; }

        public DateTime OccuredAt { get; }
    }
}
