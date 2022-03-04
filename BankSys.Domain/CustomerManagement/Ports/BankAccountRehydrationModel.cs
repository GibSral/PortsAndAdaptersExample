namespace BankSys.Domain.CustomerManagement.Ports;

public class BankAccountRehydrationModel
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = null!;
}