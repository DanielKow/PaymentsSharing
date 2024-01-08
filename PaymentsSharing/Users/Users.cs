namespace PaymentsSharing.Users;

internal class Users
{
    private readonly List<User> _users = [];
    
    public void Handle(UserCreated userCreated)
    {
        _users.Add(new User(userCreated.Username, userCreated.Password, userCreated.IsMeatEater));
    }
    
    public void Handle(UserDeleted userDeleted)
    {
        _users.RemoveAll(user => user.Username == userDeleted.Username);
    }
    
    public IReadOnlyCollection<User> GetAll()
    {
        return _users;
    }
}