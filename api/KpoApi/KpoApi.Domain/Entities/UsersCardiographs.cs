using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpoApi.Domain.Entities;

public record UserCardiograph
{
    [Key]
    public int UsersCardiographsId { get; init; }
    public Guid UserUuid { get; init; }
    public string CardiographId { get; init; }
}