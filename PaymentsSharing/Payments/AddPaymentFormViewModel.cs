using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal class AddPaymentFormViewModel(CurrentPerson currentPerson, Persons.Persons persons)
{
    public IEnumerable<Person> Payers { get; set; } = [currentPerson.Person ?? new Person("", false)];
    public IEnumerable <Person> Consumers { get; set; } = persons.All;
    public decimal Amount { get; set; }
    public decimal? AmountForMeat { get; set; }
    public string Description { get; set; } = "";
    public Person? CurrentPerson => currentPerson.Person;
    public IEnumerable<Person> AllPersons => persons.All;
    public string SelectedPayers => string.Join(", ", Payers.Select(payer => payer.Name));
    public string SelectedConsumers => string.Join(", ", Consumers.Select(consumer => consumer.Name));

    public void Save()
    {
        var payment = new Payment(
            DateTime.UtcNow,
            Payers.ToArray(),
            Consumers.ToArray(),
            Amount,
            AmountForMeat,
            Description);

        Console.WriteLine(payment);
    }
}