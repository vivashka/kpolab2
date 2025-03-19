using KpoApi.Application.Contracts.External;
using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

namespace KpoApi.Infrastructure.PostgresEfCore.Services;

public class PostgresEfCoreProvider : IPostgresEfCoreProvider
{
    private readonly IUsersRepository _usersRepository;

    public PostgresEfCoreProvider(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<User> CreateUser(User user)
    {
        var response = _usersRepository.CreateUsers(user, CancellationToken.None);

        return response;
    }

    public async Task<User?> UserAuthentication(string login, string password)
    {
        var response = await _usersRepository.UserAuthentication(login, password, CancellationToken.None);
        
        return response;
    }

    public async Task<bool> DeleteUser(User user)
    {
        bool isDeleted = await _usersRepository.DeleteUser(user, CancellationToken.None);

        return isDeleted;
    }

    public async Task<User[]> GetUsers()
    {
        User[] users = await _usersRepository.GetUsers(CancellationToken.None);

        return users;
    }
    
    
}