using System.ComponentModel.DataAnnotations;

namespace KpoApi.Domain.Entities;

public class User
{
    [Key]
    public Guid UserUuid { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public int? PhoneNumber { get; init; }
    public string FullName { get; init; }
    public Guid OrganizationUuid { get; init; }
    public string Appointment { get; init; }
}