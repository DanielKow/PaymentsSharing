using PaymentsSharing.Payments;
using PaymentsSharing.Time;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments)
{
    public IEnumerable<Refund> FromMonth(MonthAndYear monthAndYear)
    {
        decimal totalAmount = 0;
        decimal totalAmountForMeat = 0;
        
        foreach (Payment payment in payments.FromMonth(monthAndYear))
        {
            totalAmount += payment.Amount;

            if (payment.AmountForMeat is not null)
            {
                totalAmountForMeat += payment.AmountForMeat.Value;
            }
        }
        
        decimal toRefund = Math.Ceiling(totalAmount / 3);
        decimal toRefundForMeat = Math.Ceiling(totalAmountForMeat / 2);

        List<Refund> refunds = [];
        
        if (toRefund > 0)
        {
            refunds.Add(new Refund("Andrzej", "Natalia and Mikołaj", toRefund));
        }
        
        if (toRefundForMeat > 0)
        {
            refunds.Add(new Refund("Andrzej", "Mikołaj", toRefundForMeat));
        }
        
        return refunds;
    }
}