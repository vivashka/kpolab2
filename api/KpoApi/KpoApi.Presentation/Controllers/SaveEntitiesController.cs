﻿using KpoApi.Application.Contracts;
using KpoApi.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KpoApi.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class SaveEntitiesController : ControllerBase
{
    private readonly ILogger<CardiogramController> _logger;
    
    private readonly ISaveEntitiesService _saveEntitiesService;
        
    public SaveEntitiesController(ILogger<CardiogramController> logger, ISaveEntitiesService saveEntitiesService)
    {
        _logger = logger;
        _saveEntitiesService = saveEntitiesService;
    }
    
    [EnableCors]
    [HttpPost("SaveUser")]
    public async Task<IActionResult> SaveUser([FromBody] User newUser)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос сохранение пользователя");
        var request = await _saveEntitiesService.SaveUser(newUser);
        
        return Ok(request);
    }
    
    [EnableCors]
    [HttpPost("SaveCall")]
    public async Task<IActionResult> SaveCall([FromBody] Call newCall)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос сохранение вызова");
        var request = await _saveEntitiesService.SaveCall(newCall);
        
        return Ok(request);
    }
    
    [EnableCors]
    [HttpPost("SaveCardiogram")]
    public async Task<IActionResult> SaveCardiogram([FromBody] Cardiogram newCardiogram)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос сохранение кардиограммы");
        var request = await _saveEntitiesService.SaveCardiogram(newCardiogram);
        
        return Ok(request);
    }
    
    [EnableCors]
    [HttpPost("SaveCardiograph")]
    public async Task<IActionResult> SaveCardiograph([FromBody] Cardiograph newCardiograph)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос сохранение кардиографа");
        var request = await _saveEntitiesService.SaveCardiograph(newCardiograph);
        
        return Ok(request);
    }
}