namespace BankSys.Domain.CustomerManagement.Events;

public class CustomerAcquired : DomainEvent
{
    public CustomerAcquired(Guid oid, string customerId, string firstName, string lastName, string emailAddress)
    {
        Oid = oid;
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
    }

    public Guid Oid { get; }

    public string CustomerId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string EmailAddress { get; }
}