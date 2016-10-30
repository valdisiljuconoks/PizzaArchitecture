using System.Collections.Generic;
using Mediating.Sample.Features.UserManagement.Domain;

namespace Mediating.Sample.Features.UserManagement
{
    public interface IUserStorage
    {
        ICollection<User> Users { get; }

        void Save(AggregateRoot root);
    }
}
