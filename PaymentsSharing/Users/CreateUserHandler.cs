using MediatR;

namespace PaymentsSharing.Users;

internal class CreateUserHandler(ExistingUsers existingUsers, IPublisher mediator) : IRequestHandler<CreateUser>
{
    public async Task Handle(CreateUser createUser, CancellationToken cancellationToken)
    {
        if (existingUsers.Any(user => user.Username == createUser.Username))
        {
            return;
        }

        await mediator.Publish(new UserCreated(createUser.Username, createUser.Password, createUser.IsMeatEater),
            cancellationToken);
    }
}