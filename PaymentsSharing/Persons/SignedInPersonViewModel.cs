using MediatR;
using Microsoft.AspNetCore.Components;

namespace PaymentsSharing.Persons;

internal class SignedInPersonViewModel
{
    public event Action? OnPersonSignedIn;
    private readonly CurrentPerson _currentPerson;
    private readonly IPublisher _mediator;

    public SignedInPersonViewModel(CurrentPerson currentPerson, IPublisher mediator)
    {
        _currentPerson = currentPerson;
        _mediator = mediator;

        currentPerson.OnCurrentPersonChanged += () => OnPersonSignedIn?.Invoke();
    }

    public bool IsSignedIn => _currentPerson.IsSignedIn;
    public string Name => _currentPerson.Person.Name;
    
    public async Task SignOut()
    {
        await _mediator.Publish(new PersonSignedOut());
    }
}