namespace BankSys.Domain.CustomerManagement.Ports;

public class CustomerRehydrationModel
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; } = null!;
    public string emailAddress { get; set; } = null!;
    public List<BankAccountRehydrationModel> BankAccounts { get; set; } = new();
    public long Version { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}