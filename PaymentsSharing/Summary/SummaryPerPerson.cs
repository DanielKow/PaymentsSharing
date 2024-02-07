using System.Text;

namespace PaymentsSharing.Summary;

internal record SummaryPerPerson(string Person)
{
    public uint Amount { get; private set; }
    public uint AmountForMeat { get; private set; }
    
    public void AddAmount(uint amount)
    {
        Amount += amount;
    }
    
    public void AddAmountForMeat(uint amount)
    {
        AmountForMeat += amount;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        
        stringBuilder.Append(Person);
        stringBuilder.Append(" paid ");
        stringBuilder.Append(Amount.ToString("0 PLN"));
        if (AmountForMeat > 0)
        {
            stringBuilder.Append(" and ");
            stringBuilder.Append(AmountForMeat.ToString("0 PLN"));
            stringBuilder.Append(" for meat");
        }

        return stringBuilder.ToString();
    }
}