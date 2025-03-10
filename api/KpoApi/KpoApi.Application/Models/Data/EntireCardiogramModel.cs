using KpoApi.Domain.Entities;

namespace KpoApi.Application.Models.Data;

public record EntireCardiogramModel : CardiogramModel
{
    public Cardiograph Cardiograph { get; set; }
}