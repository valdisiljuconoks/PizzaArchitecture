using System;
using System.Collections.Generic;
using Mediating.Sample.Features.UserManagement.Domain;
using Mediating.Sample.Infrastructure.Queries;

namespace Mediating.Sample.Features.UserManagement
{
    public class GetAllUsersList
    {
        public class Query : IQuery<ICollection<User>> { }

        public class Handler : IQueryHandler<Query, ICollection<User>>
        {
            private readonly IUserStorage _storage;

            public Handler(IUserStorage storage)
            {
                _storage = storage;
            }

            public ICollection<User> Handle(Query command)
            {
                return _storage.Users;
            }
        }

        public class ViewModel
        {
            public ViewModel(ICollection<User> users)
            {
                if(users == null)
                    throw new ArgumentNullException(nameof(users));

                Users = users;
            }

            public ICollection<User> Users { get; private set; }
        }
    }
}
