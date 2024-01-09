namespace PaymentsSharing.EventStore;

internal interface IEventBus
{
    Task Append<TEvent>(TEvent @event) where TEvent : notnull;
}