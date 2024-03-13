using MediatR;

namespace PaymentsSharing.SignIn;

internal record AddUser(string Username, string Password) : IRequest;
