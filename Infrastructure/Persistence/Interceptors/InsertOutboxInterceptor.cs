using EFInterceptor.Model;
using EFInterceptor.Model.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace EFInterceptor.Infrastructure.Persistence.Interceptors;

public sealed class InsertOutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {

        var outboxMessages = eventData.Context!
            .ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entity => entity.Entity)
            .SelectMany(aggregate =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = aggregate.Events;

                aggregate.ClearDomainEvents();

                return domainEvents;
            })
            .Select(@event => {
                return new OutboxMessage(Guid.NewGuid(), @event.GetType().Name, JsonConvert.SerializeObject(@event), DateTime.UtcNow);
            })
            .ToList();

        eventData.Context!.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}