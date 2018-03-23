using System;
using MediatR;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserCreatedEmailSent : INotification
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

        public string Username { get; }

        public string Email { get; }

        public DateTime WhenSent { get; }

        public DateTime OccuredAt { get; } = DateTime.UtcNow;
    }
}
