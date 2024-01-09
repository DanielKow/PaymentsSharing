using MediatR;

namespace PaymentsSharing.Users;

internal class UserDeletedHandler(Users users) : INotificationHandler<UserDeleted>
{
    public Task Handle(UserDeleted userDeleted, CancellationToken cancellationToken)
    {
        var user = users.FirstOrDefault(user => user.Username == userDeleted.Username);

        if (user is null)
        {
            return Task.CompletedTask;
        }
        
        users.Remove(user);
        
        return Task.CompletedTask;
    }
}