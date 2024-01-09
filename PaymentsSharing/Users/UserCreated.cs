using MediatR;

namespace PaymentsSharing.Users;

internal record UserCreated(string Username, string Password, bool IsMeatEater) : INotification;