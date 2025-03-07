using KpoApi.Application.Models.Data;
using KpoApi.Presentation.Dtos.Response;

namespace KpoApi.Presentation.Contracts;

public interface ICardiogramMapper
{
    GetCardiogramsResponseDto MapModelToDto(CardiogramModel[] cardiogramModels);
}