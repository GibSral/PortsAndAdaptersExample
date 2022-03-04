using BankSys.Domain.CustomerManagement.DomainValues;
using BankSys.Domain.CustomerManagement.Ports;

namespace BankSys.Domain.CustomerManagement;

public class CustomerService
{
    private readonly ICustomerRepository repository;

    public CustomerService(ICustomerRepository repository)
    {
        this.repository = repository;
    }

    public void OpenNewBankAccountForCustomer(CustomerId customerId)
    {
        var (customer, version) = repository.GetCustomer(customerId);
        customer.OpenNewBankAccount(AccountNumber.Of("1234"));
        repository.Save(customer, version);
    }

    public void AcquireNewCustomer(CustomerName customerName, EmailAddress emailAddress)
    {
        var customer = Customer.AcquireNew(GetNewCustomerId(), customerName, emailAddress);
        repository.SaveNew(customer);
    }

    private CustomerId GetNewCustomerId() => CustomerId.Of(Guid.NewGuid().ToString());
}