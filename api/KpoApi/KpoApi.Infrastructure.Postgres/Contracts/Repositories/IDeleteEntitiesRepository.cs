using KpoApi.Domain.Entities;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

public interface IDeleteEntitiesRepository
{
    Task<bool> DeleteCardiogram(Guid guid, CancellationToken cancellationToken);
}