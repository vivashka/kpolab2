using KpoApi.Domain.Enums;

namespace KpoApi.Application.Models.Data;

public record CardiogramModel
{
    
    public Guid CardiogramUuid { get; init; }
    public DateTime? ReceivedTime { get; init; }
    public DateTime? MeasurementTime { get; init; }
    // public string CardiographUuid { get; init; }
    
    
    public CardiogramState CardiogramState { get; init; }
    
    public CallDataModel Call { get; init; }
    
    public ResultCardiogramModel Result { get; init; }
    
}