using MediatR;
using PaymentsSharing.EventStore;

namespace PaymentsSharing.SignIn;

internal class UserAddedHandler(Users users, IEventStore eventStore) : INotificationHandler<UserAdded>
{
    public Task Handle(UserAdded @event, CancellationToken cancellationToken)
    {
        users.AddOrUpdate(new User(@event.Username, @event.Password));
        eventStore.SaveEvent(@event, cancellationToken);
        return Task.CompletedTask;
    }
}