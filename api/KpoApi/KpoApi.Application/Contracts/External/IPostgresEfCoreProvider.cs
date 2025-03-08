using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts.External;

public interface IPostgresEfCoreProvider
{
    Task<User> CreateUser(User user);
    
    Task<User> UserAuthentication(string login, string password);
    
    Task<bool> DeleteUser(User user);

    Task<User[]> GetUsers();
}