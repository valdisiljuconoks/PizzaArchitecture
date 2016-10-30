using System.Collections.Generic;
using Mediating.Sample.Infrastructure.Events;

namespace Mediating.Sample.Features.UserManagement.Domain
{
    public class AggregateRoot
    {
        public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();
    }
}
