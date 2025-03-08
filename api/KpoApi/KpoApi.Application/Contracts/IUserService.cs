using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts;

public interface IUserService
{
    Task<User> CreateUser(User user);
    
    Task<bool> UserAuthentication(string login, string password);

    Task<bool> DeleteUser(User user, bool isAdmin);

    Task<User[]> GetUsers();
}