﻿using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Contracts;

public interface ICardiogramService
{
    Task<Cardiogram> GetCardiogram(Guid guid);
    
    Task<Cardiogram> SendCardiogram(Guid guid);
    
    Task<bool> ChangeCardiogramState(Guid guid, CardiogramState cardiogramState);
    
    Task<ResponseModel<CardiogramModel[]>> GetCardiograms(Filter filter);
}