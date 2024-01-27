namespace PaymentsSharing.Persons;

internal class CurrentPerson
{
    public void SignIn(Person person)
    {
        Person = person;
    }
    
    public void SignOut()
    {
        Person = Person.Null;
    }
    
    public Person Person { get; private set; } = Person.Null;

    public bool IsSignedIn => Person != Person.Null;
}