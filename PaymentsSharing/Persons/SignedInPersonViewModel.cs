using MediatR;

namespace PaymentsSharing.Persons;

internal class SignedInPersonViewModel(IPublisher mediator, CurrentPerson currentPerson)
{
    public bool IsSignedIn => currentPerson.Person is not null;
    public string Name => currentPerson.Person?.Name ?? string.Empty;
    
    public async Task SignIn()
    {
        await mediator.Publish(new PersonSignedIn("Andrzej"));
    }

    public async Task SignOut()
    {
        await mediator.Publish(new PersonSignedOut());
    }
}