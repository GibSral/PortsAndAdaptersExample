using AutoMapper;
using BankSys.Domain;
using BankSys.Domain.CustomerManagement;
using BankSys.Domain.CustomerManagement.DomainValues;
using BankSys.Domain.CustomerManagement.Events;
using BankSys.Domain.CustomerManagement.Ports;
using BankSys.Persistence.CustomerManagement.Scheme;

namespace BankSys.Persistence.CustomerManagement;

public class CustomerRepository : ICustomerRepository
{
    private readonly Func<CustomerContext> getCustomerContext;
    private readonly Mapper mapper;

    public CustomerRepository(Func<CustomerContext> getCustomerContext)
    {
        this.getCustomerContext = getCustomerContext;
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CustomerDb, CustomerRehydrationModel>();
            cfg.CreateMap<BankAccountDb, BankAccountRehydrationModel>();
        });
        mapper = new Mapper(mapperConfiguration);
    }

    public (Customer customer, long version) GetCustomer(Oid<Customer> id)
    {
        using var customerContext = getCustomerContext();
        var customerDb = customerContext.Customers.Find(id)!;
        mapper.Map<CustomerRehydrationModel>(customerDb);
        return (Customer.Rehydrate(mapper.Map<CustomerRehydrationModel>(customerDb)), customerDb.Version);
    }

    public (Customer customer, long version) GetCustomer(CustomerId customerId) => throw new NotImplementedException();

    public void SaveNew(Customer customer)
    {
        throw new NotImplementedException();
    }

    // Update synchronous ReadModels and WriteModel should be executed in one transaction
    public void Save(Customer customer, long expectedVersion)
    {
        using var customerContext = getCustomerContext();
        using var transaction = customerContext.Database.BeginTransaction();
        var aggregateRoot = (IAggregateRoot<Customer, DomainEvent>)customer;
        try
        {
            var customerDb = customerContext.Customers.Find(customer.Id);
            if (customerDb!.Version != expectedVersion)
            {
                throw new InvalidOperationException($"Expected version {expectedVersion} but got version {customerDb.Version}");
            }

            var uncommittedEvents = aggregateRoot.GetUncommittedEvents().ToList();
            foreach (var uncommittedEvent in uncommittedEvents)
            {
                Apply(customerDb, uncommittedEvent);
            }

            customerContext.SaveChanges();
            UpdateReadModels(uncommittedEvents);
            EmitEvents(uncommittedEvents);
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        transaction.Commit();
        aggregateRoot.OnEventsCommitted();
    }

    private static void Apply(CustomerDb customer, DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case CustomerAcquired customerAcquired:
                customer.Id = customerAcquired.Oid;
                customer.emailAddress = customerAcquired.EmailAddress;
                // ...
                break;
            case BankAccountCreated bankAccountCreated:
                customer.BankAccounts.Add(new BankAccountDb()); // Set Properties
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        throw new NotImplementedException();
    }

    // Used for synchronous CQRS
    private void UpdateReadModels(ICollection<DomainEvent> uncommittedEvents)
    {
        throw new NotImplementedException();
    }

    // Notify other services -> can update other read models in an asynchronous way -> eventual consistency
    private void EmitEvents(ICollection<DomainEvent> uncommittedEvents)
    {
        throw new NotImplementedException();
    }
}