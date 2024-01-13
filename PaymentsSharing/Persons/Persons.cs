namespace PaymentsSharing.Persons;

internal class Persons
{
    private readonly List<Person> _persons = [
        new Person("Natalia", false),
        new Person("Mikołaj", true),
        new Person("Andrzej", true)
    ];

    public IReadOnlyCollection<Person> All => _persons;
    public IReadOnlyCollection<Person> MeatEaters => _persons.Where(person => person.IsMeatEater).ToArray();
}