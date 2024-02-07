using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class PaymentsListViewModel(Payments payments, CurrentPerson currentPerson, Persons.Persons persons)
{
    public MonthAndYear MonthAndYear { get; set; } = MonthAndYear.Now;
    
    public IEnumerable<Payment> Payments =>
        payments.FromMonth(currentPerson.Person, MonthAndYear).OrderBy(payment => payment.CreatedAt);

    public string WhoPaid(Payment payment) =>
        string.Join(", ", payment.Payers.Select(person => person.Name));

    public string WhoConsumed(Payment payment) => payment.Consumers.SequenceEqual(persons.Everyone)
        ? "Everyone"
        : string.Join(", ", payment.Consumers.Select(person => person.Name));

    public string HowMuch(Payment payment) =>
        $"{payment.Amount:0 PLN} {payment.AmountForMeat?.ToString("+ 0 PLN") ?? ""}";

    public string When(Payment payment) => payment.CreatedAt.ToString("dd.MM.yyyy");

    public string ForWhat(Payment payment) => payment.Description;
}