using MediatR;

namespace PaymentsSharing.Persons;

internal record PersonSignedIn(Person Person) : INotification;
