using NMolecules.DDD;
using static Dawn.Guard;

namespace BankSys.Domain.CustomerManagement.DomainValues;

[ValueObject]
public sealed class Money : IEquatable<Money>
{
    private Money(decimal amount)
    {
    }

    public decimal Value { get; }
    
    public static Money Of(decimal amount)
    {
        Argument(amount, "Amount must be higher than 0").Require(IsValid(amount));
        return new Money(amount);
    }

    private static bool IsValid(decimal amount) => amount > 0;

    public bool Equals(Money? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Money other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();
}