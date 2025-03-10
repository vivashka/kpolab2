using KpoApi.Application.Models.Data;
using KpoApi.Domain.Enums;

namespace KpoApi.Application.Contracts.External;

public interface IPostgresProvider
{
    Task<EntireCardiogramModel?> GetCardiogram(Guid guid, CancellationToken cancellationToken);
    
    Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState, CancellationToken cancellationToken);
    
    Task<CardiogramModel[]> GetCardiogramsByFilter(Filter filter, CancellationToken cancellationToken);
}