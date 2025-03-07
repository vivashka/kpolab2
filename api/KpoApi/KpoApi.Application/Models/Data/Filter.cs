namespace KpoApi.Application.Models.Data;

public record Filter
{
    public DateTime DateFrom { get; init; }
    
    public DateTime DateTo { get; init; }
    
    public SortAttribute SortAttribute { get; init; }
    
    public SortMode SortMode { get; init; }
}

public enum SortAttribute
{
    ReceivedTime = 0,
    SsmpNumber = 1
}

public enum SortMode
{
    DESC = 0,
    ASC = 1
}