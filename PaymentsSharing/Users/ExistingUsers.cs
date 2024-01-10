using System.Collections;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace PaymentsSharing.Users;

internal class ExistingUsers : IEnumerable<User>
{
    private readonly List<User> _users = [];

    public IReadOnlyCollection<User> GetAll()
    {
        return _users;
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
    
    public void Remove(User user)
    {
        _users.Remove(user);
    }

    public IEnumerator<User> GetEnumerator()
    {
        return _users.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}