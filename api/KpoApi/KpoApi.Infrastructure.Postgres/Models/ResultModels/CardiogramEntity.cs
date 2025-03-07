
namespace KpoApi.Models.ResultModels;

public record CardiogramEntity : CallDataEntity
{
    
    public Guid CardiogramUuid { get; init; }
    public DateTime? ReceivedTime { get; init; }
    public DateTime? MeasurementTime { get; init; }
    // public string CardiographUuid { get; init; }
    public string CallUuid { get; init; }
    
    public int CardiogramState { get; init; }
    
    public Guid ResultCardiogramUuid { get; init; }
    
    public string? Description { get; init; }
    
    public string? DiagnosisMain { get; init; }
}