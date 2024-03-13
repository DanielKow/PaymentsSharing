namespace PaymentsSharing.EventStore;

internal record Event(Guid Id, string Type, string Data);