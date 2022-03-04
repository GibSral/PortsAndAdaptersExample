namespace BankSys.Domain;

internal interface IApply<in TEvent>
{
    void Apply(TEvent @event);
}