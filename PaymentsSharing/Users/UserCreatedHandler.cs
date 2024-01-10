using MediatR;

namespace PaymentsSharing.Users;

internal class UserCreatedHandler(ExistingUsers existingUsers) : INotificationHandler<UserCreated>
{
    public Task Handle(UserCreated userCreated, CancellationToken cancellationToken)
    {
        existingUsers.Add(new User(userCreated.Username, userCreated.Password, userCreated.IsMeatEater));
        return Task.CompletedTask;
    }
}