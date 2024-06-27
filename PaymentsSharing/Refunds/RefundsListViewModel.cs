using PaymentsSharing.Time;

namespace PaymentsSharing.Refunds;

internal class RefundsListViewModel(Refunds refunds)
{
    public MonthAndYear MonthAndYear { get; set; } = MonthAndYear.Now;
    
    public IEnumerable<Refund> Refunds => refunds.FromMonth(MonthAndYear);
}