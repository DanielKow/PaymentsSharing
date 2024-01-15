using MediatR;

namespace PaymentsSharing.Persons;

internal record PersonSignedIn(string Name) : INotification;
