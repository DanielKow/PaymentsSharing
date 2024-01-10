using MediatR;

namespace PaymentsSharing.Users;

internal class UserDeletedHandler(ExistingUsers existingUsers) : INotificationHandler<UserDeleted>
{
    public Task Handle(UserDeleted userDeleted, CancellationToken cancellationToken)
    {
        var user = existingUsers.FirstOrDefault(user => user.Username == userDeleted.Username);

        if (user is null)
        {
            return Task.CompletedTask;
        }
        
        existingUsers.Remove(user);
        
        return Task.CompletedTask;
    }
}