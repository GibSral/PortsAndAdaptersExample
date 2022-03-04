using Dawn;
using NMolecules.DDD;

namespace BankSys.Domain.CustomerManagement.DomainValues;

[ValueObject]
public sealed class CustomerName : IEquatable<CustomerName>
{
    public CustomerName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public static CustomerName? Of(string firstName, string lastName)
    {
        Guard.Argument(firstName, nameof(firstName)).NotEmpty().NotWhiteSpace();
        Guard.Argument(lastName, nameof(lastName)).NotEmpty().NotWhiteSpace();
        return new CustomerName(firstName, lastName);
    }

    public bool Equals(CustomerName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FirstName == other.FirstName && LastName == other.LastName;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is CustomerName other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (FirstName.GetHashCode() * 397) ^ LastName.GetHashCode();
        }
    }
}