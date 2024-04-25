using EFInterceptor.Model.Abstractions;

namespace EFInterceptor.Model;

public class AggregateRoot
{
    private readonly IList<IDomainEvent> _events = [];

    public Guid Id { get; protected set; }
    public IReadOnlyCollection<IDomainEvent> Events => _events.ToList();

    public void AddEvent<T>(T @event) where T : IDomainEvent
    {
        _events.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }
}