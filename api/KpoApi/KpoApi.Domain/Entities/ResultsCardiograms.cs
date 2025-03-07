namespace KpoApi.Domain.Entities;

public record ResultsCardiogram
{
    private string _description = string.Empty;
    private string _diagnosisMain = string.Empty;
    
    public Guid ResultCardiogramUuid { get; init; }
    public string? Description => _description;
    public string? DiagnosisMain => _diagnosisMain;

    public void ChangeDescription(string text)
    {
        _description = text;
    }
    public void ChangeDiagnosisMain(string text)
    {
        _diagnosisMain = text;
    }
}