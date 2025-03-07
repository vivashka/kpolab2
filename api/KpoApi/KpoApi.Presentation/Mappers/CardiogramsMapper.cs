using KpoApi.Application.Models.Data;
using KpoApi.Presentation.Contracts;
using KpoApi.Presentation.Dtos.Response;

namespace KpoApi.Presentation.Mappers;

public class CardiogramsMapper : ICardiogramMapper
{
    public GetCardiogramsResponseDto MapModelToDto(CardiogramModel[] cardiogramModels)
    {
        CardiogramModel[]? data = cardiogramModels.Length is not 0
            ? cardiogramModels
            : null;
        
        
        return new GetCardiogramsResponseDto()
        {
            IsSuccess = data is not null,
            Content = data,
            TotalCount = data.Length
        };
    }
}