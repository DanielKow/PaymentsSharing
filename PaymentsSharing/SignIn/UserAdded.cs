using MediatR;

namespace PaymentsSharing.SignIn;

internal record UserAdded(string Username, string Password) : INotification;
