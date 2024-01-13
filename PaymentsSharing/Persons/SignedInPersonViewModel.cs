using MediatR;

namespace PaymentsSharing.Persons;

internal class SignedInPersonViewModel(IPublisher mediator, CurrentPerson currentPerson)
{
    public CurrentPerson CurrentPerson { get; } = currentPerson;
    
    public void SignIn()
    {
        mediator.Publish(new PersonSignedIn(new Person("Andrzej", true)));
    }
}