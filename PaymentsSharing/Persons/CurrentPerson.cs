namespace PaymentsSharing.Persons;

internal class CurrentPerson
{
    private Person? _person;
    
    public void SignIn(Person person)
    {
        _person = person;
    }
    
    public void SignOut()
    {
        _person = null;
    }
    
    public Person? Person => _person;
    public bool IsSignedIn => _person is not null;
}