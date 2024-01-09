using MediatR;

namespace PaymentsSharing.Users;

internal record DeleteUser(string Username) : IRequest;