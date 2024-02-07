using PaymentsSharing.Time;

namespace PaymentsSharing.Refunds;

internal class RefundsTableViewModel(Refunds refunds)
{
    public MonthAndYear MonthAndYear { get; set; } = MonthAndYear.Now;
    
    public IEnumerable<string> From => refunds.FromMonth(MonthAndYear).Select(refund => refund.From).Distinct();
    public IEnumerable<string> To => refunds.FromMonth(MonthAndYear).Select(refund => refund.To).Distinct();

    public string ToReturn(string from, string to) =>
        refunds.FromMonth(MonthAndYear).FirstOrDefault(refund => refund.From == from && refund.To == to)?.Amount.ToString("0 PLN") ?? "-";
}