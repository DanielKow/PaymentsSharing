using MediatR;

namespace PaymentsSharing.Users;

internal record CreateUser(string Username, string Password, bool IsMeatEater) : IRequest;