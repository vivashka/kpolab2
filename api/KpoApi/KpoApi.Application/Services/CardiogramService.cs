using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Services;

public class CardiogramService : ICardiogramService, ISaveEntitiesService
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

    public Task<Organization[]> GetOrganizations()
    {
        var organizationArray = _postgresProvider.GetOrganizations(CancellationToken.None);

        return organizationArray;
    }

    public Task<User[]> GetUsers(Guid organizationGuid)
    {
        var usersArray = _postgresProvider.GetUsers(organizationGuid ,CancellationToken.None);

        return usersArray;
    }

    public Task<Cardiograph[]> GetCardiographs(Guid userGuid)
    {
        var cardiographs = _postgresProvider.GetCardiographs(userGuid ,CancellationToken.None);

        return cardiographs;
    }

    public Task<Cardiogram[]> GetCardiograms(string serialNumber)
    {
        var cardiograms = _postgresProvider.GetCardiograms(serialNumber ,CancellationToken.None);

        return cardiograms;
    }

    public Task<User[]> GetUsersByCardiograms(Guid cardiogramUuid)
    {
        var cardiograms = _postgresProvider.GetUsersByCardiograms(cardiogramUuid ,CancellationToken.None);

        return cardiograms;
    }

    public async Task<User> SaveUser(User newUser)
    {
        var user = await _postgresProvider.SaveUser(newUser ,CancellationToken.None);

        return user;
    }
    
    public async Task<Cardiogram> SaveCardiogram(Cardiogram newCardiogram)
    {
        var cardiogram = await _postgresProvider.SaveCardiogram(newCardiogram ,CancellationToken.None);

        return cardiogram;
    }
    
    public async Task<Call> SaveCall(Call newCall)
    {
        var call = await _postgresProvider.SaveCall(newCall ,CancellationToken.None);

        return call;
    }
    
    public async Task<Cardiograph> SaveCardiograph(Cardiograph newCardiograph)
    {
        var cardiograph = await _postgresProvider.SaveCardiograph(newCardiograph ,CancellationToken.None);

        return cardiograph;
    }
}