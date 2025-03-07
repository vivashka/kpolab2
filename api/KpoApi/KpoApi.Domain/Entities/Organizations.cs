namespace KpoApi.Domain.Entities;

public record Organization
{
    public string Name { get; init; }
    public int SsmpNumber { get; init; }
    public string SsmpAdress { get; init; }
    public string PhoneContactName { get; init; }
    public int PhoneNumber { get; init; }
}