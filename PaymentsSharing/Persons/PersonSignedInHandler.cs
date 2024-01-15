using MediatR;

namespace PaymentsSharing.Persons;

internal class PersonSignedInHandler(CurrentPerson currentPerson, Persons persons) : INotificationHandler<PersonSignedIn>
{
    public Task Handle(PersonSignedIn personSignedIn, CancellationToken cancellationToken)
    {
        var person = persons.ByName(personSignedIn.Name);
        currentPerson.SignIn(person);
        return Task.CompletedTask;
    }
}