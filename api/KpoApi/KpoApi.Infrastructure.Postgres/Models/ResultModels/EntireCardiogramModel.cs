namespace KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

public class EntireCardiogramEntity
{
    // Основные поля кардиограммы
    public Guid CardiogramUuid { get; set; }
    public DateTime ReceivedTime { get; set; }
    public DateTime MeasurementTime { get; set; }
    public int CardiogramState { get; set; }
    
    public string RawCardiogram { get; set; }

    // Данные кардиографа
    public string CardiographUuid { get; set; }
    public string CardiographName { get; set; }
    public string ManufacturerName { get; set; }

    // Данные вызова (Call)
    public string CallUuid { get; set; }
    
    public int CallState { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PatientPatronymic { get; set; }
    
    public string PatientId { get; set; }
    public int PatientAge { get; set; }
    public int PatientSex { get; set; }
    public string BrigadeNumber { get; set; }
    
    public string BrigadeProfile { get; set; }
    
    public string MainChiefNumber { get; set; }
    
    public string MainChiefFullName { get; set; }
    public int YearNumber { get; set; }
    public int DayNumber { get; set; }
    
    public string RegionCode { get; set; }
    
    public string City { get; set; }
    public int SsmpNumber { get; set; }
    
    public string SsmpName { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public int ApartmentNumber { get; set; }
    public int Entrance { get; set; }
    public int Priority { get; set; }
    public string HospitalizationAddress { get; set; }
    
    public double HospitalizationDistance { get; set; }
    public string CallType { get; set; }
    public string Reason { get; set; }
    public DateTime ReceiveTime { get; set; }
    public DateTime TransferTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string CallDiagnosis { get; set; }

    //Результаты кардиограммы
    public Guid ResultCardiogramUuid { get; set; }
    public string Description { get; set; }
    public string DiagnosisMain { get; set; }
}