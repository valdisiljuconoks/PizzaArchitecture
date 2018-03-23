using System.Collections.Generic;
using MediatR;

namespace Mediating.Sample.Features.UserManagement.Domain
{
    public class AggregateRoot
    {
        public ICollection<INotification> Events { get; } = new List<INotification>();
    }
}
