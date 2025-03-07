using KpoApi.Domain.Enums;

namespace KpoApi.Domain.Entities;

public record Call
{
    public string? CallUuid { get; init; }
    public int? CallState { get; init; }
    public string? PatientName { get; init; }
    public string? PatientSurname { get; init; }
    public string? PatientPatronymic { get; init; }
    public int? PatientAge { get; init; }
    public string? PatientId { get; init; }
    public int PatientSex { get; init; }
    public string? BrigadeNumber { get; init; }
    public string? BrigadeProfile { get; init; }
    public string? MainChiefNumber { get; init; }
    public string? MainChiefFullName { get; init; }
    public int YearNumber { get; init; }
    public int DayNumber { get; init; }
    public int? RegionCode { get; init; }
    public string? City { get; init; }
    public int? SsmpNumber { get; init; }
    public string? SsmpName { get; init; }
    public string? Street { get; init; }
    public int? StreetNumber { get; init; }
    public int? ApartmentNumber { get; init; }
    public int? Entrance { get; init; }
    public Priority Priority { get; init; }
    public string? HospitalizationAddress { get; init; }
    public string? CallType { get; init; }
    public string? Reason { get; init; }
    public DateTime? ReceiveTime { get; init; }
    public DateTime? TransferTime { get; init; }
    public DateTime? DepartureTime { get; init; }
    public DateTime? ArrivalTime { get; init; }
    public string? CallDiagnosis { get; init; }
}