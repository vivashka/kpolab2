using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Contracts.Mappers;
using KpoApi.Contracts.Repositories;
using KpoApi.Domain.Enums;


namespace KpoApi.Services;

public sealed class PostgresProvider : IPostgresProvider
{
    private readonly ICardiogramsRepository _cardiogramsRepository;

    private readonly ICardiogramMapper _cardiogramMapper;
    
    public PostgresProvider(ICardiogramsRepository cardiogramsRepository, ICardiogramMapper cardiogramMapper)
    {
        _cardiogramsRepository = cardiogramsRepository;
        _cardiogramMapper = cardiogramMapper;
    }


    public Task<CardiogramModel> GetCardiogram(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeCardiogramState(Guid guid, CardiogramState cardiogramState, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CardiogramModel[]> GetCardiogramsByFilter(Filter filter, CancellationToken cancellationToken)
    {
        var requestResult = await _cardiogramsRepository.GetCardiograms(filter, cancellationToken);

        CardiogramModel[] cardiogramModels = new CardiogramModel[requestResult.Length];
        
        for (int i = 0; i < requestResult.Length; i++)
        {
            CardiogramModel cardiogramModel = _cardiogramMapper.MapOrderEntityToModel(requestResult[i]);
            cardiogramModels[i] = cardiogramModel;
        }

        return cardiogramModels;
    }
}