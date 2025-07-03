using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService
{
    private static readonly List<User> Users = new();

    public List<User> GetAll() => Users;
    public User? Get(int id) => Users.FirstOrDefault(u => u.Id == id);
    public void Create(User user) => Users.Add(user);
    public void Update(User user)
    {
        var index = Users.FindIndex(u => u.Id == user.Id);
        if (index != -1) Users[index] = user;
    }
    public void Delete(int id) => Users.RemoveAll(u => u.Id == id);
}
