using System.ComponentModel.DataAnnotations;

namespace KpoApi.Presentation.Dtos.Request;

public class UserDto
{
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
    
    public string CardiographId { get; init; }
}