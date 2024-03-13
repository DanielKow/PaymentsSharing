using MediatR;

namespace PaymentsSharing.SignIn;

internal class AddUserHandler(IPublisher publisher) : IRequestHandler<AddUser>
{
    public async Task Handle(AddUser command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Username) || string.IsNullOrWhiteSpace(command.Password))
        {
            return;
        }

        await publisher.Publish(new UserAdded(command.Username, command.Password), cancellationToken);
    }
}