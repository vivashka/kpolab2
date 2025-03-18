namespace KpoApi.Domain.Entities;

public record ResultsCardiogram
{
    
    public Guid ResultCardiogramUuid { get; init; }
    public string? Description { get; init; }
    public string? DiagnosisMain { get; init; }
    
}