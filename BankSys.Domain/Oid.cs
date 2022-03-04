using Dawn;
using NMolecules.DDD;

namespace BankSys.Domain;

[ValueObject]
public sealed class Oid<TEntity> : IEquatable<Oid<TEntity>>
{
    private Oid(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static Oid<TEntity> Of(Guid id)
    {
        Guard.Argument(id, nameof(id)).Require(it => !it.Equals(Guid.Empty));
        return new Oid<TEntity>(id);
    }

    public bool Equals(Oid<TEntity>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Oid<TEntity> other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();
}