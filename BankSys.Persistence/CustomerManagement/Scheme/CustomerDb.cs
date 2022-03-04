namespace BankSys.Persistence.CustomerManagement.Scheme;

public class CustomerDb
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string emailAddress { get; set; } = string.Empty;
    public List<BankAccountDb> BankAccounts { get; set; } = new();
    public long Version { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}