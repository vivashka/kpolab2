using KpoApi.Domain.Entities;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

public interface IUsersRepository
{
    Task<User> CreateUsers(User user, string CardiographId, CancellationToken cancellationToken);
    
    Task<User?> UserAuthentication(string login, string password, CancellationToken cancellationToken);
    
    Task<bool> DeleteUser(User user, CancellationToken cancellationToken);

    Task<User[]> GetUsers(CancellationToken cancellationToken);
}