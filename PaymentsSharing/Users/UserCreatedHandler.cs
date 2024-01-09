using MediatR;

namespace PaymentsSharing.Users;

internal class UserCreatedHandler(Users users) : INotificationHandler<UserCreated>
{
    public Task Handle(UserCreated userCreated, CancellationToken cancellationToken)
    {
        users.Add(new User(userCreated.Username, userCreated.Password, userCreated.IsMeatEater));
        return Task.CompletedTask;
    }
}