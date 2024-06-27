using PaymentsSharing.Payments;
using PaymentsSharing.Time;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments)
{
    public IEnumerable<Refund> FromMonth(MonthAndYear monthAndYear)
    {
        List<Refund> refunds = [];
        
        foreach (Payment payment in payments.FromMonth(monthAndYear))
        {
            
        }
        
        return refunds;
    }
}