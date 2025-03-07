using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Contracts;

public interface ICardiogramService
{
    Task<Cardiogram> GetCardiogram(Guid guid);
    
    Task<Cardiogram> SendCardiogram(Guid guid);
    
    Task<bool> ChangeCardiogramState(Guid guid);
    
    Task<ResponseModel<CardiogramModel[]>> GetCardiograms(Filter filter);
}