using KpoApi.Application.Models.Data;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Contracts.External;

public interface IPostgresProvider
{
    Task<EntireCardiogramModel?> GetCardiogram(Guid guid, CancellationToken cancellationToken);
    
    Task<Call[]> GetCalls(CancellationToken cancellationToken);
    
    Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState, CancellationToken cancellationToken);
    
    Task<CardiogramModel[]> GetCardiogramsByFilter(Filter filter, CancellationToken cancellationToken);
    
    Task<Organization[]> GetOrganizations(CancellationToken cancellationToken);

    Task<User[]> GetUsers(Guid organizationGuid, CancellationToken cancellationToken);
    
    Task<Cardiograph[]> GetCardiographs(Guid? userGuid, CancellationToken cancellationToken);
    
    Task<Cardiogram[]> GetCardiograms(string serialNumber, CancellationToken cancellationToken);
    
    Task<User[]> GetUsersByCardiograms(Guid cardiogramUuid, CancellationToken cancellationToken);
    
    Task<User> SaveUser(User newUser, CancellationToken cancellationToken);
    
    Task<Cardiogram> SaveCardiogram(Cardiogram newCardiogram, CancellationToken cancellationToken);
    
    Task<Call> SaveCall(Call newCall, CancellationToken cancellationToken);
    
    Task<Cardiograph> SaveCardiograph(Cardiograph newCardiograph, CancellationToken cancellationToken);
    
    Task<ResultsCardiogram> SaveResult(ResultsCardiogram newResults, CancellationToken cancellationToken);
}