namespace PaymentsSharing.Refunds;

internal class RefundsTableViewModel(Refunds refunds)
{
    public IEnumerable<string> From => refunds.Select(refund => refund.From).ToArray();
    public IEnumerable<string> To => refunds.Select(refund => refund.To).ToArray();
    
}