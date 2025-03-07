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

    public Task<bool> CreateUser(User user)
    {
        var response = _usersRepository.CreateUsers(user, CancellationToken.None);

        return response;
    }

    public Task<bool> UserAuthentication(string login, string password)
    {
        throw new NotImplementedException();
    }
}