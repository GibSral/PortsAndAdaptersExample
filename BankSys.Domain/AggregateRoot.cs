using System.Reflection;
using BankSys.Domain.CustomerManagement.Ports;

namespace BankSys.Domain;

public abstract class AggregateRoot<TAggregate, TEvent> : IAggregateRoot<TAggregate, TEvent> where TAggregate : notnull where TEvent : notnull
{
    private const string ApplyMethodName = "Apply";
    private readonly Dictionary<Type, ApplyInvocation> applyInvocations = new();
    private readonly Oid<TAggregate> id;
    private readonly List<TEvent> uncommittedEvents = new();
    private int initialVersion = -1;
    private int version;

    protected AggregateRoot(Oid<TAggregate> id)
    {
        this.id = id;
        Id = id;
    }

    public Oid<TAggregate> Id { get; }

    Oid<TAggregate> IAggregateRoot<TAggregate, TEvent>.Id => id;

    int IAggregateRoot<TAggregate, TEvent>.Version => version;

    int IAggregateRoot<TAggregate, TEvent>.InitialVersion => initialVersion;

    IReadOnlyCollection<TEvent> IAggregateRoot<TAggregate, TEvent>.GetUncommittedEvents() => uncommittedEvents;

    void IAggregateRoot<TAggregate, TEvent>.OnEventsCommitted() => uncommittedEvents.Clear();

    protected void RaiseEvent(TEvent @event)
    {
        ApplyEvent(@event);
        uncommittedEvents.Add(@event);
    }

    public void Replay(IEnumerable<TEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event);
            initialVersion++;
        }
    }

    private void ApplyEvent(TEvent @event)
    {
        var eventType = @event.GetType();
        if (!applyInvocations.TryGetValue(eventType, out var applyInvocation))
        {
            var domainObjectType = GetType().GetTypeInfo();
            applyInvocation = domainObjectType.DeclaredMethods
                .Where(IsApplyMethod)
                .Where(method => HasCorrectParameters(method, eventType))
                .Select(method => new ApplyInvocation(method, this))
                .FirstOrDefault();

            if (applyInvocation == null)
            {
                throw new InvalidOperationException($"Type {domainObjectType} does not implement IApply<{eventType}>");
            }

            applyInvocations.Add(eventType, applyInvocation);
        }

        applyInvocation.Execute(@event);
        version++;
    }

    private static bool IsApplyMethod(MethodInfo method)
    {
        var isApplyMethod = method.Name == ApplyMethodName;
        return isApplyMethod || method.Name.EndsWith($".{ApplyMethodName}");
    }

    private static bool HasCorrectParameters(MethodBase method, Type eventType)
    {
        var parameters = method.GetParameters();
        var isApplyMethod = parameters.Length == 1;
        if (isApplyMethod)
        {
            var firstParameter = parameters[0];
            isApplyMethod = firstParameter.ParameterType == eventType;
        }

        return isApplyMethod;
    }

    private class ApplyInvocation
    {
        private readonly object domainObject;
        private readonly MethodInfo method;

        internal ApplyInvocation(MethodInfo method, object domainObject)
        {
            this.method = method;
            this.domainObject = domainObject;
        }

        internal void Execute(object @event) => method.Invoke(domainObject, new[] { @event });
    }
}