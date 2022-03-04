using BankSys.Domain.CustomerManagement.DomainValues;
using BankSys.Domain.CustomerManagement.Events;
using BankSys.Domain.CustomerManagement.Ports;
using Dawn;
using NMolecules.DDD.Attributes;

namespace BankSys.Domain.CustomerManagement;

[AggregateRoot]
public class Customer : AggregateRoot<Customer, DomainEvent>, IApply<CustomerAcquired>, IApply<BankAccountCreated>
{
    private CustomerId? customerId;
    private CustomerName? customerName;
    private EmailAddress? emailAddress;
    private readonly List<BankAccount> associatedBankAccounts = new();

    private Customer(Oid<Customer> id)
        : base(id)
    {
    }

    private Customer(CustomerRehydrationModel rehydrationModel)
        : base(Oid<Customer>.Of(rehydrationModel.Id))
    {
        customerId = CustomerId.Of(rehydrationModel.CustomerId);
        customerName = CustomerName.Of(rehydrationModel.FirstName, rehydrationModel.LastName);
        emailAddress = EmailAddress.Of(rehydrationModel.emailAddress);
        foreach (var bankAccountRehydrationModel in rehydrationModel.BankAccounts)
        {
            var bankAccount = BankAccount.Rehydrate(bankAccountRehydrationModel);
            associatedBankAccounts.Add(bankAccount);
        }
    }

    public static Customer Rehydrate(CustomerRehydrationModel customerRehydrationModel)
    {
        return new Customer(customerRehydrationModel);
    }

    public static Customer AcquireNew(CustomerId customerId, CustomerName customerName, EmailAddress emailAddress)
    {
        var customer = new Customer(Oid<Customer>.Of(Guid.NewGuid()));
        customer.Acquire(customerId, customerName, emailAddress);
        return customer;
    }

    private void Acquire(CustomerId customerId, CustomerName customerName, EmailAddress emailAddress)
    {
        RaiseEvent(new CustomerAcquired(Id.Value, customerId.Value, customerName.FirstName, customerName.LastName, emailAddress.Value));
    }

    public void OpenNewBankAccount(AccountNumber accountNumber)
    {
        Guard.Argument(() => accountNumber).NotIn(associatedBankAccounts.Select(it => it.AccountNumber));
        RaiseEvent(new BankAccountCreated(Guid.NewGuid(), accountNumber.Value));
    }

    public void Apply(CustomerAcquired @event)
    {
        customerId = CustomerId.Of(@event.CustomerId);
        customerName = CustomerName.Of(@event.FirstName, @event.LastName);
        emailAddress = EmailAddress.Of(@event.EmailAddress);
    }

    public void Apply(BankAccountCreated @event)
    {
        var bankAccount = new BankAccount(Oid<BankAccount>.Of(@event.Id), AccountNumber.Of(@event.BankAccountName));
        associatedBankAccounts.Add(bankAccount);
    }
}