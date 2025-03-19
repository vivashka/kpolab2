using System.ComponentModel.DataAnnotations;

namespace KpoApi.Domain.Entities;

public class User
{
    [Key]
    public Guid UserUuid { get; init; }
    
    [Required]
    public string Login { get; init; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    public long? PhoneNumber { get; init; }
    
    [Required]
    public string FullName { get; init; }
    
    [Required]
    public Guid OrganizationUuid { get; init; }
    
    [Required]
    public string Appointment { get; init; }
    
}