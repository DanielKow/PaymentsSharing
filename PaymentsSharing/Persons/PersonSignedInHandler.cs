using MediatR;

namespace PaymentsSharing.Persons;

internal class PersonSignedInHandler(CurrentPerson currentPerson) : INotificationHandler<PersonSignedIn>
{
    public Task Handle(PersonSignedIn personSignedIn, CancellationToken cancellationToken)
    {
        currentPerson.SignIn(personSignedIn.Person);
        return Task.CompletedTask;
    }
}