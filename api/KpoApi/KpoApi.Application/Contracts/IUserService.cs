using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts;

public interface IUserService
{
    Task<bool> CreateUsers(User user, CancellationToken cancellationToken);
    
    Task<bool> UserAuthentication(string login, string password, CancellationToken cancellationToken);
}