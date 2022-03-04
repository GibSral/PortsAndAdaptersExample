namespace BankSys.Domain.CustomerManagement.Ports;

public interface IAggregateRoot<TAggregate, TEvent> where TAggregate : notnull where TEvent : notnull
{
    Oid<TAggregate> Id { get; }

    int Version { get; }

    int InitialVersion { get; }

    IReadOnlyCollection<TEvent> GetUncommittedEvents();

    void OnEventsCommitted();

    void Replay(IEnumerable<TEvent> events);
}