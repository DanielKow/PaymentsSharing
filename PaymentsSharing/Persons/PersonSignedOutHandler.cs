using MediatR;

namespace PaymentsSharing.Persons;

internal class PersonSignedOutHandler(CurrentPerson currentPerson) : INotificationHandler<PersonSignedOut>
{
    public Task Handle(PersonSignedOut notification, CancellationToken cancellationToken)
    {
        currentPerson.SignOut();
        return Task.CompletedTask;
    }
}