using KpoApi.Application.Models.Data;
using KpoApi.Models.ResultModels;

namespace KpoApi.Contracts.Mappers;

public interface ICardiogramMapper
{
     CardiogramModel MapOrderEntityToModel(CardiogramEntity cardiogramEntity);
     
     CardiogramEntity MapOrderModelToEntity(CardiogramModel cardiogramEntity);
}