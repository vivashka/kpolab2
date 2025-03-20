using KpoApi.Application.Models.Data;
using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

public interface ICardiogramsRepository
{
    Task<EntireCardiogramEntity?> GetCardiogram(Guid guid, CancellationToken cancellationToken);
    
    Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState, CancellationToken cancellationToken);
    
    Task<CardiogramEntity[]> GetCardiograms(Filter filter, CancellationToken cancellationToken);

    Task<Organization[]> GetOrganizations(CancellationToken cancellationToken);

    Task<User[]> GetUsers(Guid? organizationGuid, CancellationToken cancellationToken);
    
    Task<Cardiograph[]> GetCardiographs(Guid? userGuid, CancellationToken cancellationToken);
    
    Task<Cardiogram[]> GetCardiograms(string serialNumber, CancellationToken cancellationToken);
    
    Task<User[]> GetUsersByCardiograms(Guid cardiogramUuid, CancellationToken cancellationToken);

    Task<Call[]> GetCalls(CancellationToken cancellationToken);
}