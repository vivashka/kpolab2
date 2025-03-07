namespace KpoApi.Domain.Entities;

public record Cardiograph
{
    public string? SerialNumber { get; init; }
    public string CardiographName { get; init; }
    public string? ManufacturerName { get; init; }
}