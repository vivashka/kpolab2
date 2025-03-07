using KpoApi.Domain.Entities;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

public interface IUsersRepository
{
    Task<User> CreateUsers(User user, CancellationToken cancellationToken);
    
    Task<bool> UserAuthentication(string login, string password, CancellationToken cancellationToken);
}