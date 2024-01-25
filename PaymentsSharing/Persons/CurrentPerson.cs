namespace PaymentsSharing.Persons;

internal class CurrentPerson
{
    public event Action? OnCurrentPersonChanged;
    
    public void SignIn(Person person)
    {
        Person = person;
        OnCurrentPersonChanged?.Invoke();
    }
    
    public void SignOut()
    {
        Person = Person.Null;
        OnCurrentPersonChanged?.Invoke();
    }
    
    public Person Person { get; private set; } = Person.Null;

    public bool IsSignedIn => Person != Person.Null;
}