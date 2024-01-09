using MediatR;
using Microsoft.AspNetCore.Identity;

namespace PaymentsSharing.Users;

internal class CreateUserHandler(Users users, IPublisher mediator) : IRequestHandler<CreateUser>
{
    private readonly PasswordHasher<string> _passwordHasher = new();

    public async Task Handle(CreateUser createUser, CancellationToken cancellationToken)
    {
        if (users.Any(user => user.Username == createUser.Username))
        {
            return;
        }

        var passwordHash = _passwordHasher.HashPassword(createUser.Username, createUser.Password);
        await mediator.Publish(new UserCreated(createUser.Username, passwordHash, createUser.IsMeatEater),
            cancellationToken);
    }
}