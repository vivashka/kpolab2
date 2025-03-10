using KpoApi.Application.Models.Data;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;

public interface ICardiogramsMapper
{
     CardiogramModel MapOrderEntityToModel(CardiogramEntity cardiogramEntity);
     
     CardiogramEntity MapOrderModelToEntity(CardiogramModel cardiogramEntity);
}