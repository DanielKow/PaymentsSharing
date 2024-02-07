namespace PaymentsSharing.Refunds;

internal class RefundsTableViewModel(Refunds refunds)
{
    
    
    public IEnumerable<string> From => refunds.Select(refund => refund.From).Distinct();
    public IEnumerable<string> To => refunds.Select(refund => refund.To).Distinct();

    public string ToReturn(string from, string to) =>
        refunds.FirstOrDefault(refund => refund.From == from && refund.To == to)?.Amount.ToString("0 PLN") ?? "-";
}