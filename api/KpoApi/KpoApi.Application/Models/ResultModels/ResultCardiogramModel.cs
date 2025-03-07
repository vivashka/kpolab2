namespace KpoApi.Application.Models.ResultModels;

public class ResultCardiogramModel
{
    public Guid ResultCardiogramUuid { get; init; }
    
    public string? Description { get; init; }
    
    public string? DiagnosisMain { get; init; }
}