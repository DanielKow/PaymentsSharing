using MediatR;
using Microsoft.AspNetCore.Components;

namespace PaymentsSharing.Persons;

internal class SignedInPersonViewModel(CurrentPerson currentPerson, IPublisher mediator, NavigationManager navigationManager)
{
    public bool IsSignedIn => currentPerson.IsSignedIn;
    public string Name => currentPerson.Person.Name;
    
    public async Task SignOut()
    {
        await mediator.Publish(new PersonSignedOut());
        navigationManager.NavigateTo("/", true, true);
    }
}