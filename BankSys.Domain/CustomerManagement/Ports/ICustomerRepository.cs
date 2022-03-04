using BankSys.Domain.CustomerManagement.DomainValues;

namespace BankSys.Domain.CustomerManagement.Ports;

public interface ICustomerRepository
{
    (Customer customer, long version) GetCustomer(Oid<Customer> id);
    (Customer customer, long version) GetCustomer(CustomerId customerId);
    void Save(Customer customer, long expectedVersion);
    void SaveNew(Customer customer);
}