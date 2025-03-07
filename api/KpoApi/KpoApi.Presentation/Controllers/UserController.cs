using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KpoApi.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<CardiogramController> _logger;

    private readonly IUserService _userService;

    public UserController(ILogger<CardiogramController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на получение кардиограмм");
        var request = await _userService.CreateUser(user);

        return Ok(request);
    }
}