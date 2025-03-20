using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Infrastructure.PostgresEfCore.Services;

public sealed class PostgresProvider : IPostgresProvider
{
    private readonly ICardiogramsRepository _cardiogramsRepository;

    private readonly ICardiogramsMapper _cardiogramsMapper;

    private readonly IEntireCardiogramMapper _entireCardiogramMapper;

    private readonly ISaveEntitiesRepository _saveEntitiesRepository;
    private readonly IDeleteEntitiesRepository _deleteEntitiesRepository;

    public PostgresProvider(ICardiogramsRepository cardiogramsRepository, ICardiogramsMapper cardiogramsMapper,
        IEntireCardiogramMapper entireCardiogramMapper, ISaveEntitiesRepository saveEntitiesRepository, IDeleteEntitiesRepository deleteEntitiesRepository)
    {
        _cardiogramsRepository = cardiogramsRepository;
        _cardiogramsMapper = cardiogramsMapper;
        _entireCardiogramMapper = entireCardiogramMapper;
        _saveEntitiesRepository = saveEntitiesRepository;
        _deleteEntitiesRepository = deleteEntitiesRepository;
    }


    public async Task<EntireCardiogramModel?> GetCardiogram(Guid guid, CancellationToken cancellationToken)
    {
        EntireCardiogramEntity? requestResult = await _cardiogramsRepository.GetCardiogram(guid, cancellationToken);

        if (requestResult != null)
        {
            var entireModel = _entireCardiogramMapper.MapOrderEntityToModel(requestResult);

            return entireModel;
        }
        else
        {
            return null;
        }
    }

    public async Task<Call[]> GetCalls(CancellationToken cancellationToken)
    {
        var request = await _cardiogramsRepository.GetCalls(cancellationToken);

        return request;
    }

    public async Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState, CancellationToken cancellationToken)
    {
        var request = await _cardiogramsRepository.ChangeCardiogramState(guid, cardiogramState, cancellationToken);

        return request;
    }

    public async Task<CardiogramModel[]> GetCardiogramsByFilter(Filter filter, CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetCardiograms(filter, cancellationToken);

        CardiogramModel[] cardiogramModels = new CardiogramModel[requestResult.Length];

        for (int i = 0; i < requestResult.Length; i++)
        {
            CardiogramModel cardiogramModel = _cardiogramsMapper.MapOrderEntityToModel(requestResult[i]);
            cardiogramModels[i] = cardiogramModel;
        }

        return cardiogramModels;
    }

    public async Task<Organization[]> GetOrganizations(CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetOrganizations(cancellationToken);

        return requestResult;
    }

    public async Task<User[]> GetUsers(Guid? organizationGuid, CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetUsers(organizationGuid, cancellationToken);

        return requestResult;
    }

    public async Task<Cardiograph[]> GetCardiographs(Guid? userGuid, CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetCardiographs(userGuid, cancellationToken);

        return requestResult;
    }

    public async Task<Cardiogram[]> GetCardiograms(string serialNumber, CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetCardiograms(serialNumber, cancellationToken);

        return requestResult;
    }

    public async Task<User[]> GetUsersByCardiograms(Guid cardiogramUuid, CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetUsersByCardiograms(cardiogramUuid, cancellationToken);

        return requestResult;
    }

    public async Task<User> SaveUser(User newUser, CancellationToken cancellationToken)
    {
        var requestResult = await _saveEntitiesRepository.SaveUser(newUser, cancellationToken);

        return requestResult;
    }

    public async Task<Cardiogram> SaveCardiogram(Cardiogram newCardiogram, CancellationToken cancellationToken)
    {
        var requestResult = await _saveEntitiesRepository.SaveCardiogram(newCardiogram, cancellationToken);

        return requestResult;
    }

    public async Task<Call> SaveCall(Call newCall, CancellationToken cancellationToken)
    {
        var requestResult = await _saveEntitiesRepository.SaveCall(newCall, cancellationToken);

        return requestResult;
    }

    public async Task<Cardiograph> SaveCardiograph(Cardiograph newCardiograph, CancellationToken cancellationToken)
    {
        var requestResult = await _saveEntitiesRepository.SaveCardiograph(newCardiograph, cancellationToken);

        return requestResult;
    }
    
    public async Task<ResultsCardiogram> SaveResult(ResultsCardiogram newResult, CancellationToken cancellationToken)
    {
        var requestResult = await _saveEntitiesRepository.SaveResult(newResult, cancellationToken);

        return requestResult;
    }
    
    public async Task<bool> DeleteCardiogram(Guid guid, CancellationToken cancellationToken)
    {
        var requestResult = await _deleteEntitiesRepository.DeleteCardiogram(guid, cancellationToken);

        return requestResult;
    }
}