namespace BankSys.Domain.CustomerManagement.Events;

public class BankAccountCreated : DomainEvent
{
    public BankAccountCreated(Guid id, string bankAccountName)
    {
        Id = id;
        BankAccountName = bankAccountName;
    }

    public Guid Id { get; }

    public string BankAccountName { get; }
}