using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts;

public interface ISaveEntitiesService
{
    Task<ResponseModel<User>> SaveUser(User newUser);
    
    Task<ResponseModel<Cardiogram>> SaveCardiogram(Cardiogram newCardiogram);
    
    Task<ResponseModel<Call>> SaveCall(Call newCall);
    
    Task<ResponseModel<Cardiograph>> SaveCardiograph(Cardiograph newCardiogram);
    
    Task<ResponseModel<ResultsCardiogram>> SaveResult(ResultsCardiogram newCardiogram);
}