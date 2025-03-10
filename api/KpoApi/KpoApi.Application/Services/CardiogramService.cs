using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Services;

public class CardiogramService : ICardiogramService
{
    private readonly IPostgresProvider _postgresProvider;
    
    public CardiogramService(IPostgresProvider postgresProvider, IPostgresEfCoreProvider efCoreProvider)
    {
        _postgresProvider = postgresProvider;
    }
    
    public async Task<EntireCardiogramModel?> GetCardiogram(Guid guid)
    {
        var entireModel = await _postgresProvider.GetCardiogram(guid, CancellationToken.None);

        return entireModel;
    }

    public Task<Cardiogram> SendCardiogram(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeCardiogramState(Guid guid, CardiogramState cardiogramState)
    {
        var isChanged = _postgresProvider.ChangeCardiogramState(guid, (int)cardiogramState, CancellationToken.None);

        return isChanged;
    }

    public async Task<ResponseModel<CardiogramModel[]>> GetCardiograms(Filter filter)
    {
        if (string.IsNullOrWhiteSpace(filter.DateFrom.ToString()))
        {
            filter.DateFrom = DateTime.MinValue;
        }
        
        if (string.IsNullOrWhiteSpace(filter.DateTo.ToString()))
        {
            filter.DateTo = DateTime.Now;
        }
        var cardiograms = await _postgresProvider.GetCardiogramsByFilter(filter, CancellationToken.None);

        return new ResponseModel<CardiogramModel[]> (cardiograms,
            true,
            null);
    }


}