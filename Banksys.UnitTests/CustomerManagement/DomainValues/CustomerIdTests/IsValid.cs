using BankSys.Domain.CustomerManagement.DomainValues;
using FluentAssertions;
using NUnit.Framework;

namespace BankSys.UnitTests.CustomerManagement.DomainValues.CustomerIdTests;

[TestFixture]
public class IsValid
{
    [TestCase("SomeString")]
    [TestCase("customer-")]
    [TestCase("customer-a")]
    public void IsValid_WithInvalidId_ReturnsFalse(string invalidId)
    {
        CustomerId.IsValid(invalidId).Should().BeFalse();
    }

    [TestCase("customer-1")]
    [TestCase("customer-12")]
    public void IsValid_WithValidId_ReturnsTrue(string validId)
    {
        CustomerId.IsValid(validId).Should().BeTrue();
    }
}