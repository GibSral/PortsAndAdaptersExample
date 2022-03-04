using BankSys.Domain.CustomerManagement.DomainValues;
using FluentAssertions;
using NUnit.Framework;

namespace BankSys.UnitTests.CustomerManagement.DomainValues.EmailAddressTests;

[TestFixture]
public class IsValid
{
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("This is definitely no email address")]
    public void IsValid_WithInvalidEmailAddress_ReturnsFalse(string invalidEmailAddress)
    {
        EmailAddress.IsValid(invalidEmailAddress).Should().BeFalse();
    }

    [Test]
    public void IsValid_WithValidEmailAddress_ReturnsTrue()
    {
        EmailAddress.IsValid("lb@wps.de").Should().BeTrue();
    }
}