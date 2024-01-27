using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Summary;

internal class SummaryViewModel(Payments.Payments payments)
{
    
    public string Amount => payments.All.Sum(payment => payment.Amount).ToString("0.00 PLN");
    public string AmountForMeat => payments.All.Sum(payment => payment.AmountForMeat ?? 0).ToString("0.00 PLN");
    public string CurrentMonthAndYearTitle => DateTime.Now.ToString("MMMM yyyy");

}