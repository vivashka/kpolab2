using KpoApi.Domain.Enums;

namespace KpoApi.Application.Models.Data;

public record CallDataModel
{
    public string CallUuid { get; init; }
    public int CallState { get; set; }
    public string? CallType { get; init; }
    public string? CallDiagnosis { get; init; }
    
    public string? PatientName { get; init; }
    public string? PatientSurname { get; init; }
    public string? PatientPatronymic { get; init; }
    public int? PatientAge { get; init; }
    public int PatientSex { get; init; }
    public string PatientId { get; set; }
    public string? BrigadeNumber { get; init; }
    public string BrigadeProfile { get; set; }
    public string MainChiefNumber { get; set; }
    public string MainChiefFullName { get; set; }
    public int YearNumber { get; init; }
    public int DayNumber { get; init; }
    public string RegionCode { get; set; }
    public string City { get; set; }
    public int? SsmpNumber { get; init; }
    public string SsmpName { get; set; }
    public string? Street { get; init; }
    public int? StreetNumber { get; init; }
    public int? ApartmentNumber { get; init; }
    public int? Entrance { get; init; }
    public Priority Priority { get; init; }
    public string? HospitalizationAddress { get; init; }
    public double HospitalizationDistance { get; set; }
    public string? Reason { get; init; }
    public DateTime? ReceiveTime { get; init; }
    public DateTime? TransferTime { get; init; }
    public DateTime? DepartureTime { get; init; }
    public DateTime? ArrivalTime { get; init; }
    
}