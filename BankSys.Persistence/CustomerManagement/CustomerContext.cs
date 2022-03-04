using BankSys.Persistence.CustomerManagement.Scheme;
using Microsoft.EntityFrameworkCore;

namespace BankSys.Persistence.CustomerManagement;

public class CustomerContext : DbContext
{
    public CustomerContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "bank.db");
    }
    
    public string DbPath { get; }
    public DbSet<CustomerDb> Customers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}