using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSys.Persistence.CustomerManagement.Scheme;

public class BankAccountDb
{
    [Key]
    [Column("BankAccountId")]
    public Guid Id { get; set; }
    public string AccountName { get; set; } = null!;
}