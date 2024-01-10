using MediatR;

namespace PaymentsSharing.Users;

internal class DeleteUserHandler(ExistingUsers existingUsers, IPublisher mediator) : IRequestHandler<DeleteUser>
{
    public async Task Handle(DeleteUser deleteUser, CancellationToken cancellationToken)
    {
        if (existingUsers.Any(user => user.Username == deleteUser.Username))
        {
            await mediator.Publish(new UserDeleted(deleteUser.Username), cancellationToken);
        }
    }
}