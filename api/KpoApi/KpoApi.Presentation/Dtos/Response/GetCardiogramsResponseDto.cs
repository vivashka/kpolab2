using System.Text.Json.Serialization;
using KpoApi.Application.Models.Data;

namespace KpoApi.Presentation.Dtos.Response;

public sealed record GetCardiogramsResponseDto : BaseResponseDto
{
    [JsonPropertyName("totalCount")]
    public required int TotalCount { get; set; }
    
    [JsonPropertyName("content")]
    public required CardiogramModel[] Content { get; set; }
    
    
}