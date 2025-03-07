﻿using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Services;

public class CardiogramService : ICardiogramService
{
    private readonly IPostgresProvider _postgresProvider;
    
    public CardiogramService(IPostgresProvider postgresProvider)
    {
        _postgresProvider = postgresProvider;
    }
    
    public Task<Cardiogram> GetCardiogram(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<Cardiogram> SendCardiogram(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeCardiogramState(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<CardiogramModel[]>> GetCardiograms(Filter filter)
    {
        var cardiograms = await _postgresProvider.GetCardiogramsByFilter(filter, CancellationToken.None);

        return new ResponseModel<CardiogramModel[]> (cardiograms,
            true,
            null);
    }
}