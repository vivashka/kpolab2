using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts;

public interface ISaveEntitiesService
{
    Task<User> SaveUser(User newUser);
    
    Task<Cardiogram> SaveCardiogram(Cardiogram newCardiogram);
    
    Task<Call> SaveCall(Call newCall);
    
    Task<Cardiograph> SaveCardiograph(Cardiograph newCardiogram);
    
    Task<ResultsCardiogram> SaveResult(ResultsCardiogram newCardiogram);
}