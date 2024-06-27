using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class PaymentsListViewModel(Payments payments)
{
    public MonthAndYear MonthAndYear { get; set; } = MonthAndYear.Now;
    
    public IEnumerable<Payment> Payments =>
        payments.FromMonth(MonthAndYear).OrderBy(payment => payment.CreatedAt);

    public string Total => Payments.Sum(payment => payment.Amount + (payment.AmountForMeat ?? 0)).ToString("0 PLN");

    public string HowMuch(Payment payment) =>
        $"{payment.Amount:0 PLN} {payment.AmountForMeat?.ToString("+ 0 PLN") ?? ""}";

    public string When(Payment payment) => payment.CreatedAt.ToString("dd.MM.yyyy");

    public string ForWhat(Payment payment) => payment.Description;

}