using KpoApi.Domain.Entities;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

public interface ISaveEntitiesRepository
{
    Task<User> SaveUser(User newUser, CancellationToken cancellationToken);
    
    Task<Cardiogram> SaveCardiogram(Cardiogram newCardiogram, CancellationToken cancellationToken);
    
    Task<Call> SaveCall(Call newCall, CancellationToken cancellationToken);
    
    Task<Cardiograph> SaveCardiograph(Cardiograph newCardiogram, CancellationToken cancellationToken);
}