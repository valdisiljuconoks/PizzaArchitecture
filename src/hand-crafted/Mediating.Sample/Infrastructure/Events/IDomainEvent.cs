using System;

namespace Mediating.Sample.Infrastructure.Events
{
    public interface IDomainEvent
    {
        DateTime OccuredAt { get; }
    }
}
