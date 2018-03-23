using System;
using System.Collections.Generic;
using System.Linq;
using Mediating.Sample.Features.UserManagement.Domain;
using MediatR;

namespace Mediating.Sample.Features.UserManagement
{
    internal class InMemoryUserStorage : IUserStorage
    {
        private readonly IMediator _mediator;
        private readonly List<User> _users;

        public InMemoryUserStorage(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            // initial seed
            _users = new List<User>
                     {
                         User.Create("john", "john@server.lv", "John", "Doe"),
                         User.Create("mary", "mary@another-server.lv", "Mary", "Doe")
                     };
        }

        public ICollection<User> Users => _users;

        public void Save(AggregateRoot root)
        {
            // simulation of the actual database call
            // ...

            // dispatch of the events
            if(!root.Events.Any())
                return;

            while (root.Events.Any())
            {
                var @event = root.Events.FirstOrDefault();
                if(@event == null)
                    continue;

                _mediator.Publish(@event);
                root.Events.Remove(@event);
            }
        }
    }
}
