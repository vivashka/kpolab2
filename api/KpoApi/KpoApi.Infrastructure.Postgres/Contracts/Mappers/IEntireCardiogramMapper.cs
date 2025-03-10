using KpoApi.Application.Models.Data;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

namespace KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;

public interface IEntireCardiogramMapper
{
    EntireCardiogramModel MapOrderEntityToModel(EntireCardiogramEntity cardiogramEntity);
     
    EntireCardiogramEntity MapOrderModelToEntity(EntireCardiogramModel cardiogramEntity);
}