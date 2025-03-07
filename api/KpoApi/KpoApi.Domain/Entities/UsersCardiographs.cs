namespace KpoApi.Domain.Entities;

public record UserCardiograph
{
    public int? UsersCardiographsId { get; init; }
    public Guid UserUuid { get; init; }
    public string CardiographId { get; init; }
}