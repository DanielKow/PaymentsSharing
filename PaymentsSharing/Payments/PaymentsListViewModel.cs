using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class PaymentsListViewModel(Payments payments, CurrentPerson currentPerson, Persons.Persons persons)
{
    public IEnumerable<Payment> Payments => payments.FromMonth(currentPerson.Person, _monthAndYear);

    private MonthAndYear _monthAndYear = MonthAndYear.Now;

    public void ToPreviousMonth()
    {
        _monthAndYear = _monthAndYear.Previous;
    }

    public void ToNextMonth()
    {
        _monthAndYear = _monthAndYear.Next;
    }

    public bool NextMonthIsAvailable => _monthAndYear != MonthAndYear.Now;
    
    public string MonthAndYearTitle => new DateTime(_monthAndYear.Year, _monthAndYear.Month, 1).ToString("MMMM yyyy");

    public string WhoPaid(Payment payment) =>
        string.Join(", ", payment.Payers.Select(person => person.Name));

    public string WhoConsumed(Payment payment) => payment.Consumers.SequenceEqual(persons.Everyone)
        ? "Everyone"
        : string.Join(", ", payment.Consumers.Select(person => person.Name));

    public string HowMuch(Payment payment) =>
        $"{payment.Amount:0.00 PLN} {payment.AmountForMeat?.ToString("+ 0.00 PLN") ?? ""}";

    public string When(Payment payment) => payment.CreatedAt.ToString("dd.MM.yyyy");

    public string ForWhat(Payment payment) => payment.Description;
}