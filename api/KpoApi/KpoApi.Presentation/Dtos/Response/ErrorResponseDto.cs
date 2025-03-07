using System.Text.Json.Serialization;

namespace KpoApi.Presentation.Dtos.Response;

public sealed record ErrorResponseDto
{
    [JsonPropertyName("code")]
    public int ErrorCode { get; init; }
    
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; init; } = string.Empty;
}