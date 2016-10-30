using System;

namespace Mediating.Sample.Features.UserManagement.Domain
{
    public class User : AggregateRoot
    {
        protected User() { }

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static User Create(string username, string email, string firstName, string lastName)
        {
            if(string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if(string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));
            if(string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if(string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));

            var result = new User
                         {
                             Id = Guid.NewGuid(),
                             Username = username,
                             Email = email,
                             FirstName = firstName,
                             LastName = lastName
                         };

            result.Events.Add(new UserCreatedEvent(username,  email));

            return result;
        }
    }
}
