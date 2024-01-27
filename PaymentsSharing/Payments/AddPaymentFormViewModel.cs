using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal class AddPaymentFormViewModel
{
    private readonly CurrentPerson _currentPerson;
    private readonly Persons.Persons _persons;
    
    public AddPaymentFormViewModel(CurrentPerson currentPerson, Persons.Persons persons)
    {
        _currentPerson = currentPerson;
        _persons = persons;
        Payers = _currentPerson.IsSignedIn ? [_currentPerson.Person] : [];
        Consumers = _persons.Everyone;
    }
    
    public IEnumerable<Person> Payers { get; set; }
    public IEnumerable<Person> Consumers { get; set; }
    public decimal Amount { get; set; }
    public decimal? AmountForMeat { get; set; }
    public string Description { get; set; } = "";
    public Person? CurrentPerson => _currentPerson.Person;
    public bool IsCurrentPersonEatMeat => _currentPerson.Person.IsMeatEater;
    public IEnumerable<Person> AllPersons => _persons.Everyone;
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