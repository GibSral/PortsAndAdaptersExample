using System.Text.RegularExpressions;
using Dawn;
using NMolecules.DDD;

namespace BankSys.Domain.CustomerManagement.DomainValues;

[ValueObject]
public sealed class CustomerId : IEquatable<CustomerId>
{
    private static readonly Regex validationRegex = new Regex(@"customer-\d+", RegexOptions.Compiled);
    private CustomerId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static CustomerId Of(string id)
    {
        Guard.Argument(id, nameof(id)).Require(IsValid(id));
        return new CustomerId(id);
    }

    public static bool IsValid(string id) => validationRegex.IsMatch(id);

    public bool Equals(CustomerId? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((CustomerId)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();
}