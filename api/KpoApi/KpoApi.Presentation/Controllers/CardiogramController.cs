using KpoApi.Application.Contracts;
using KpoApi.Application.Models.Data;
using KpoApi.Domain.Enums;
using KpoApi.Presentation.Contracts;
using KpoApi.Presentation.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KpoApi.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class CardiogramController : ControllerBase
{
    private readonly ILogger<CardiogramController> _logger;

    private readonly ICardiogramService _cardiogramService;

    private readonly ICardiogramMapper _cardiogramMapper;

    public CardiogramController(ICardiogramService cardiogramService, ILogger<CardiogramController> logger,
        ICardiogramMapper cardiogramMapper)
    {
        _cardiogramService = cardiogramService;
        _logger = logger;
        _cardiogramMapper = cardiogramMapper;
    }


    [HttpPost("GetListCardiograms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCardiogramsResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetCardiogramsResponseDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetCardiogramsResponseDto))]
    public async Task<IActionResult> GetListCardiograms([FromBody] Filter filter)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение кардиограмм");
        var request = await _cardiogramService.GetCardiograms(filter);

        if (request.IsSuccess)
        {
            var cardiogramModels = _cardiogramMapper.MapModelToDto(request.SuccessEntity);

            return Ok(cardiogramModels);
        }

        return Ok(new GetCardiogramsResponseDto
        {
            TotalCount = 0,
            Content = [],
            IsSuccess = false
        });
    }
    
    [HttpPut("GetListCardiograms")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(bool))]
    public async Task<IActionResult> GetListCardiograms(Guid guid, CardiogramState cardiogramState)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на изменение кардиограммы");
        var request = await _cardiogramService.ChangeCardiogramState(guid, cardiogramState);
        
        return Ok(request);
        
    }
}