namespace PaymentsSharing.Refunds;

internal record Refund(string From, string To)
{
    public decimal Amount { get; private set; }
    
    public void Increase(decimal amount)
    {
        Amount += amount;
    }
    
    public void Decrease(decimal amount)
    {
        Amount -= amount;
    }
    
    public Refund WithAmount(decimal amount)
    {
        Amount = amount;
        return this;
    }

    public Refund WithIntegerAmount()
    {
        Amount = decimal.Ceiling(Amount);
        return this;
    }
    
    public void Reset()
    {
        Amount = 0;
    }
}