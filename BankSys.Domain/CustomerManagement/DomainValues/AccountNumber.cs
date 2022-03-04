using Dawn;
using NMolecules.DDD;

namespace BankSys.Domain.CustomerManagement.DomainValues;

[ValueObject]
public sealed class AccountNumber : IEquatable<AccountNumber>
{
    private AccountNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static AccountNumber Of(string name)
    {
        Guard.Argument(name, nameof(name)).NotEmpty().NotWhiteSpace();
        return new AccountNumber(name);
    }
    
    public bool Equals(AccountNumber? other)
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

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is AccountNumber other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();
}