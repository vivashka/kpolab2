using KpoApi.Application.Models.Data;
using KpoApi.Models.Enums;
using KpoApi.Models.ResultModels;

namespace KpoApi.Contracts.Repositories;

public interface ICardiogramsRepository
{
    Task<CardiogramEntity?> GetCardiogram(Guid guid, CancellationToken cancellationToken);
    
    Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState, CancellationToken cancellationToken);
    
    Task<CardiogramEntity[]> GetCardiograms(Filter filter, CancellationToken cancellationToken);
}