using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Models;

namespace KpoApi.Infrastructure.PostgresEfCore.Repositories;

public class UserRepository : IUsersRepository
{
    private AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public Task<bool> CreateUsers(User user, CancellationToken cancellationToken)
    {
        var response = _appDbContext.Users.Add(user);

        _appDbContext.SaveChanges();

        return Task.FromResult(true);
    }

    public Task<bool> UserAuthentication(string login, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}