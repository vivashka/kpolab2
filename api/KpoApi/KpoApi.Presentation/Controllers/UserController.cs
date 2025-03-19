using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Domain.Entities;
using KpoApi.Presentation.Dtos.Request;
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
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        var user = new User()
        { 
            UserUuid = userDto.UserUuid,
            Login = userDto.Login,
            Password = userDto.Password, 
            PhoneNumber = userDto.PhoneNumber,
            FullName = userDto.FullName,
            OrganizationUuid = userDto.OrganizationUuid,
            Appointment = userDto.Appointment
        };
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information, "Поступил запрос на регистрацию пользователя");
        var request = await _userService.CreateUser(user, userDto.CardiographId);

        return Ok(request);
    }
    
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] User user, bool isAdmin)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на удаление пользователя");
        var request = await _userService.DeleteUser(user, isAdmin);

        return Ok(request);
    }
    
    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на удаление пользователя");
        
        var request = await _userService.GetUsers();

        return Ok(request);
    }
    
    [HttpPost("Authentication")]
    public async Task<IActionResult> Authentication([FromBody] AuthenticationDto authenticationDto)
    {
        _logger.Log(LogLevel.Information, "Поступил запрос на авторизацию пользователя");
        
        var request = await _userService.UserAuthentication(authenticationDto.Login, authenticationDto.Password);

        return Ok(request);
    }
}