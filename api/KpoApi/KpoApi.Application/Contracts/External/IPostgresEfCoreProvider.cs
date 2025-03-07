using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts.External;

public interface IPostgresEfCoreProvider
{
    Task<bool> CreateUser(User user);
    
    Task<bool> UserAuthentication(string login, string password);
}