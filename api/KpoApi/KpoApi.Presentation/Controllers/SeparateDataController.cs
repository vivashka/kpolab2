using KpoApi.Application.Contracts;
using KpoApi.Domain.Entities;
using KpoApi.Presentation.Dtos.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KpoApi.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class SeparateDataController : ControllerBase
{
    private readonly ILogger<CardiogramController> _logger;

    private readonly ICardiogramService _cardiogramService;

    public SeparateDataController(ILogger<CardiogramController> logger, ICardiogramService cardiogramService)
    {
        _logger = logger;
        _cardiogramService = cardiogramService;
    }
    
    [EnableCors]
    [HttpGet("GetOrganizations")]
    public async Task<IActionResult> GetOrganizations()
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение организаций");
        var request = await _cardiogramService.GetOrganizations();

        if (request.Length > 0)
        {
            return Ok(request);
        }

        return BadRequest();

    }
    
    [EnableCors]
    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers([FromQuery] Guid organizationGuid)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение организаций");
        var request = await _cardiogramService.GetUsers(organizationGuid);

        if (request.Length > 0)
        {
            return Ok(request);
        }

        return BadRequest();

    }
    
    [EnableCors]
    [HttpGet("GetCardiographs")]
    public async Task<IActionResult> GetCardiographs([FromQuery] Guid userGuid)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение организаций");
        var request = await _cardiogramService.GetCardiographs(userGuid);

        if (request.Length > 0)
        {
            return Ok(request);
        }

        return BadRequest();

    }
    
    [EnableCors]
    [HttpGet("GetCardiograms")]
    public async Task<IActionResult> GetCardiograms([FromQuery] string serialNumber)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение организаций");
        var request = await _cardiogramService.GetCardiograms(serialNumber);

        if (request.Length > 0)
        {
            return Ok(request);
        }

        return BadRequest();

    }
}