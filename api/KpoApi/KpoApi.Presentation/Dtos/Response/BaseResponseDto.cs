using System.Text.Json.Serialization;

namespace KpoApi.Presentation.Dtos.Response;

public record BaseResponseDto
{
    [JsonPropertyName("success")]
    public required bool IsSuccess { get; init; }
    
    [JsonPropertyName("error")]
    public IReadOnlyList<ErrorResponseDto>? Errors { get; init; }
}