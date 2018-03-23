using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mediating.Sample.Features.UserManagement.Domain;
using MediatR;

namespace Mediating.Sample.Features.UserManagement
{
    public class GetAllUsersList
    {
        public class Query : IRequest<ICollection<User>> { }

        public class Handler : IRequestHandler<Query, ICollection<User>>
        {
            private readonly IUserStorage _storage;

            public Handler(IUserStorage storage)
            {
                _storage = storage;
            }

            public Task<ICollection<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_storage.Users);
            }
        }

        public class ViewModel
        {
            public ViewModel(ICollection<User> users)
            {
                Users = users ?? throw new ArgumentNullException(nameof(users));
            }

            public ICollection<User> Users { get; }
        }
    }
}
