namespace KpoApi.Presentation.Dtos.Request;

public record AuthenticationDto
{
    public string Login { get; init; }
    public string Password { get; init; }
}