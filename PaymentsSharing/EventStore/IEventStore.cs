using MediatR;

namespace PaymentsSharing.EventStore;

internal interface IEventStore
{
    Task SaveEvent<T>(T @event, CancellationToken cancellationToken) where T : INotification;
}