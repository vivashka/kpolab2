using KpoApi.Application.Models.Data;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

public interface ICardiogramsRepository
{
    Task<EntireCardiogramEntity?> GetCardiogram(Guid guid, CancellationToken cancellationToken);
    
    Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState, CancellationToken cancellationToken);
    
    Task<CardiogramEntity[]> GetCardiograms(Filter filter, CancellationToken cancellationToken);
}