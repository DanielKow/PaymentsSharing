using MediatR;

namespace PaymentsSharing.Users;

internal record UserDeleted(string Username) : INotification;