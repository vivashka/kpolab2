using KpoApi.Domain.Enums;

namespace KpoApi.Domain.Entities;

public record Cardiogram
{
    
    public Guid CardiogramUuid { get; init; }
    public DateTime? ReceivedTime { get; init; }
    public DateTime? MeasurementTime { get; init; }
    public string CardiographUuid { get; init; }
    public string CallUuid { get; init; }
    public CardiogramState CardiogramState { get; init; }

    public Guid ResultCardiogramUuid { get; init; }
    public string? RawCardiogram { get; init; }
}