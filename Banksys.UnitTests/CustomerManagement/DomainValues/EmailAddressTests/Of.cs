using BankSys.Domain.CustomerManagement.DomainValues;
using FluentAssertions;
using NUnit.Framework;

namespace BankSys.UnitTests.CustomerManagement.DomainValues.EmailAddressTests;

[TestFixture]
public class Of
{
    [Test]
    public void Of_WithValidEmailAddress_ReturnsEmailAddress()
    {
        const string validEmailAddress = "lb@wps.de";
        var emailAddress = EmailAddress.Of(validEmailAddress);
        emailAddress.Value.Should().Be(validEmailAddress);
    }
}