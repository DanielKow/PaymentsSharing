namespace PaymentsSharing.SignIn;

internal class Users
{
    private readonly List<User> _users = [
        new User("Natalia", "test"),
        new User("Mikołaj", "test"),
        new User("Andrzej", "test")
    ];
    
    public bool CheckCredentials(User user)
    {
        return _users.Contains(user);
    }
}