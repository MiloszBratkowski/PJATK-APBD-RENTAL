using PJATK_APBD_RENTAL.Models.Actors;

namespace PJATK_APBD_RENTAL.Services;

public class UserManager
{
    private readonly Dictionary<Guid, User> _users = new();

    public void AddUser(User user)
    {
        if (!_users.ContainsKey(user.Id))
        {
            _users.Add(user.Id, user);
        }
    }

    public List<User> AllUsers => _users.Values.ToList();

    public User? GetById(Guid id) => _users.GetValueOrDefault(id);
    
    public User? GetByLastName(string lastName) => 
        _users.Values.FirstOrDefault(u => u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
}