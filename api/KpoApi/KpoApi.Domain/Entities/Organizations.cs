using System.ComponentModel.DataAnnotations;

namespace KpoApi.Domain.Entities;

public record Organization
{
    [Key]
    public Guid OrganizationUuid { get; init; }
    public string Name { get; init; }
    public int SsmpNumber { get; init; }
    public string SsmpAdress { get; init; }
    public string PhoneContactName { get; init; }
    public long PhoneNumber { get; init; }
}