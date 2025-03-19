using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace KpoApi.Infrastructure.PostgresEfCore.Repositories;

public class UserRepository : IUsersRepository
{
    private AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<User> CreateUsers(User user, string CardiographId, CancellationToken cancellationToken)
    {
        var responseUsers = await _appDbContext.Users.AddAsync(user, cancellationToken);

        var userCardiographModel = new UserCardiograph()
        {
            UserUuid = user.UserUuid,
            CardiographId = CardiographId
        };
        
        await _appDbContext.UsersCardiographs.AddAsync(userCardiographModel, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return responseUsers.Entity;
    }

    public async Task<User?> UserAuthentication(string login, string password, CancellationToken cancellationToken)
    {
        var response = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password)!;

        return response;
    }

    public async Task<bool> DeleteUser(User user, CancellationToken cancellationToken)
    {
        var response = _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return response.State == EntityState.Deleted;
    }

    public async Task<User[]> GetUsers(CancellationToken cancellationToken)
    {
        var response = await _appDbContext.Users.ToListAsync(cancellationToken);    
        return response.ToArray();
    }
}