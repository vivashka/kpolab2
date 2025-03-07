using KpoApi.Application.Contracts.External;
using KpoApi.Domain.Entities;
using KpoApi.Presentation.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KpoApi.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<CardiogramController> _logger;

    private readonly IPostgresEfCoreProvider _efCoreProvider;

    public UserController(ILogger<CardiogramController> logger, IPostgresEfCoreProvider efCoreProvider)
    {
        _logger = logger;
        _efCoreProvider = efCoreProvider;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение кардиограмм");
        var request = await _efCoreProvider.CreateUser(user);

        return Ok(request);
    }
}