namespace PaymentsSharing.SignIn;

internal class Users
{
    private readonly Dictionary<string, string> _users = [];
    
    
    public void AddOrUpdate(User user)
    {
        _users[user.Name] = user.Password;
    }
    
    public bool CheckCredentials(User user)
    {
        if (!_users.TryGetValue(user.Name, out string? password))
        {
            return false;
        }
        
        return password == user.Password;
    }
}