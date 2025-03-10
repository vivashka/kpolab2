using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

namespace KpoApi.Infrastructure.PostgresEfCore.Services;

public sealed class PostgresProvider : IPostgresProvider
{
    private readonly ICardiogramsRepository _cardiogramsRepository;

    private readonly ICardiogramsMapper _cardiogramsMapper;

    private readonly IEntireCardiogramMapper _entireCardiogramMapper;
    
    public PostgresProvider(ICardiogramsRepository cardiogramsRepository, ICardiogramsMapper cardiogramsMapper, IEntireCardiogramMapper entireCardiogramMapper)
    {
        _cardiogramsRepository = cardiogramsRepository;
        _cardiogramsMapper = cardiogramsMapper;
        _entireCardiogramMapper = entireCardiogramMapper;
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
}