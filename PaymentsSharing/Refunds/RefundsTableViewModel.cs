using PaymentsSharing.Time;

namespace PaymentsSharing.Refunds;

internal class RefundsTableViewModel(Refunds refunds)
{
    public MonthAndYear MonthAndYear { get; set; } = MonthAndYear.Now;
    
    public IEnumerable<Refund> Refunds => refunds.FromMonth(MonthAndYear);
}