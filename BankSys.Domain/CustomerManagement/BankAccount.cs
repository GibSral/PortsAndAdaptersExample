using BankSys.Domain.CustomerManagement.DomainValues;
using BankSys.Domain.CustomerManagement.Ports;
using NMolecules.DDD;

namespace BankSys.Domain.CustomerManagement;

[Entity]
public class BankAccount
{
    private BankAccount(BankAccountRehydrationModel rehydrationModel)
    {
        Id = Oid<BankAccount>.Of(rehydrationModel.Id);
        AccountNumber = AccountNumber.Of(rehydrationModel.AccountNumber);
    }

    internal BankAccount(Oid<BankAccount> id, AccountNumber accountNumber)
    {
        Id = id;
        AccountNumber = accountNumber;
    }

    public Oid<BankAccount> Id { get; }
    public AccountNumber AccountNumber { get; }

    public static BankAccount Rehydrate(BankAccountRehydrationModel bankAccountRehydrationModel)
    {
        return new BankAccount(bankAccountRehydrationModel);
    }
}