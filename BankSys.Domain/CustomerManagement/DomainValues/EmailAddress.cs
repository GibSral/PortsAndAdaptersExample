using System.Text.RegularExpressions;
using Dawn;
using NMolecules.DDD;

namespace BankSys.Domain.CustomerManagement.DomainValues;

[ValueObject]
public sealed class EmailAddress : IEquatable<EmailAddress>
{
    private static readonly Regex validationRegex =
        new("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled);

    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static EmailAddress Of(string emailAddress)
    {
        Guard.Argument(emailAddress, nameof(emailAddress)).Require(IsValid);
        return new EmailAddress(emailAddress);
    }

    public static bool IsValid(string emailAddress) => validationRegex.IsMatch(emailAddress);

    public bool Equals(EmailAddress? other)
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

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is EmailAddress other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(validationRegex, Value);
}