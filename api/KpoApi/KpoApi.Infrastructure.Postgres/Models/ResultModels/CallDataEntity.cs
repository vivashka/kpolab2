

namespace KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

public record CallDataEntity
{
    public string? PatientName { get; init; }
    public string? PatientSurname { get; init; }
    public string? PatientPatronymic { get; init; }
    public int? PatientAge { get; init; }
    public int PatientSex { get; init; }
    public string? BrigadeNumber { get; init; }
    public int YearNumber { get; init; }
    public int DayNumber { get; init; }
    public int? SsmpNumber { get; init; }
    public string? Street { get; init; }
    public int? StreetNumber { get; init; }
    public int? ApartmentNumber { get; init; }
    public int? Entrance { get; init; }
    public int Priority { get; init; }
    public string? HospitalizationAddress { get; init; }
    public string? CallType { get; init; }
    public string? Reason { get; init; }
    public DateTime? ReceiveTime { get; init; }
    public DateTime? TransferTime { get; init; }
    public DateTime? DepartureTime { get; init; }
    public DateTime? ArrivalTime { get; init; }
    public string? CallDiagnosis { get; init; }
}