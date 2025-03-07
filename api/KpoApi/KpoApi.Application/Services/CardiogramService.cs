using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Services;

public class CardiogramService : ICardiogramService, IUserService
{
    private readonly IPostgresProvider _postgresProvider;

    private readonly IPostgresEfCoreProvider _efCoreProvider;
    
    public CardiogramService(IPostgresProvider postgresProvider, IPostgresEfCoreProvider efCoreProvider)
    {
        _postgresProvider = postgresProvider;
        _efCoreProvider = efCoreProvider;
    }
    
    public Task<Cardiogram> GetCardiogram(Guid guid)
    {
        throw new NotImplementedException();
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
        var cardiograms = await _postgresProvider.GetCardiogramsByFilter(filter, CancellationToken.None);

        return new ResponseModel<CardiogramModel[]> (cardiograms,
            true,
            null);
    }

    public Task<bool> CreateUsers(User user, CancellationToken cancellationToken)
    {
        var response = _efCoreProvider.CreateUser(user);
        return response;
    }

    public Task<bool> UserAuthentication(string login, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}