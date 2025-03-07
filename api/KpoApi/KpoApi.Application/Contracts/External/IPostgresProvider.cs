using KpoApi.Application.Models.Data;
using KpoApi.Domain.Enums;

namespace KpoApi.Application.Contracts.External;

public interface IPostgresProvider
{
    Task<CardiogramModel> GetCardiogram(Guid guid, CancellationToken cancellationToken);
    
    Task<bool> ChangeCardiogramState(Guid guid, CardiogramState cardiogramState, CancellationToken cancellationToken);
    
    Task<CardiogramModel[]> GetCardiogramsByFilter(Filter filter, CancellationToken cancellationToken);
}