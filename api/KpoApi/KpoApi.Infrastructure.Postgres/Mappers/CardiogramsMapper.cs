using KpoApi.Application.Models.Data;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;
using CardiogramState = KpoApi.Domain.Enums.CardiogramState;
using Priority = KpoApi.Domain.Enums.Priority;

namespace KpoApi.Infrastructure.PostgresEfCore.Mappers;

public class CardiogramsMapper : ICardiogramsMapper
{

    public CardiogramModel MapOrderEntityToModel(CardiogramEntity cardiogramEntity)
    {
        CardiogramModel cardiogramModel = new CardiogramModel()
        {
            CardiogramUuid = cardiogramEntity.CardiogramUuid,
            ReceivedTime = cardiogramEntity.ReceivedTime,
            MeasurementTime = cardiogramEntity.MeasurementTime,
            CardiogramState = (CardiogramState)cardiogramEntity.CardiogramState,
            
            Result = new ResultCardiogramModel()
            {
                Description = cardiogramEntity.Description,
                DiagnosisMain = cardiogramEntity.DiagnosisMain,
                ResultCardiogramUuid = cardiogramEntity.ResultCardiogramUuid
            },
            
            Call = new CallDataModel
            {
                CallUuid = cardiogramEntity.CallUuid,
                CallType = cardiogramEntity.CallType,
                CallDiagnosis = cardiogramEntity.CallDiagnosis,
    
                PatientName = cardiogramEntity.PatientName,
                PatientSurname = cardiogramEntity.PatientSurname,
                PatientPatronymic = cardiogramEntity.PatientPatronymic,
                PatientAge = cardiogramEntity.PatientAge,
                PatientSex = cardiogramEntity.PatientSex,
    
                BrigadeNumber = cardiogramEntity.BrigadeNumber,
                YearNumber = cardiogramEntity.YearNumber,
                DayNumber = cardiogramEntity.DayNumber,
                SsmpNumber = cardiogramEntity.SsmpNumber,
                Street = cardiogramEntity.Street,
                StreetNumber = cardiogramEntity.StreetNumber,
                ApartmentNumber = cardiogramEntity.ApartmentNumber,
                Entrance = cardiogramEntity.Entrance,
                Priority = (Priority)cardiogramEntity.Priority,
                HospitalizationAddress = cardiogramEntity.HospitalizationAddress,
    
                Reason = cardiogramEntity.Reason,
                ReceiveTime = cardiogramEntity.ReceiveTime,
                TransferTime = cardiogramEntity.TransferTime,
                DepartureTime = cardiogramEntity.DepartureTime,
                ArrivalTime = cardiogramEntity.ArrivalTime
            }

        };
        
        
        return cardiogramModel;
    }

    public CardiogramEntity MapOrderModelToEntity(CardiogramModel cardiogramModel)
{
    return new CardiogramEntity
    {
   
        CardiogramUuid = cardiogramModel.CardiogramUuid,
        ReceivedTime = cardiogramModel.ReceivedTime,
        MeasurementTime = cardiogramModel.MeasurementTime,
        CallUuid = cardiogramModel.Call.CallUuid, 
        CardiogramState = (int)cardiogramModel.CardiogramState,
        ResultCardiogramUuid = cardiogramModel.Result.ResultCardiogramUuid,
        Description = cardiogramModel.Result?.Description, 
        DiagnosisMain = cardiogramModel.Result?.DiagnosisMain,

      
        PatientName = cardiogramModel.Call.PatientName,
        PatientSurname = cardiogramModel.Call.PatientSurname,
        PatientPatronymic = cardiogramModel.Call.PatientPatronymic,
        PatientAge = cardiogramModel.Call.PatientAge,
        PatientSex = cardiogramModel.Call.PatientSex,
        BrigadeNumber = cardiogramModel.Call.BrigadeNumber,
        YearNumber = cardiogramModel.Call.YearNumber,
        DayNumber = cardiogramModel.Call.DayNumber,
        SsmpNumber = cardiogramModel.Call.SsmpNumber,
        Street = cardiogramModel.Call.Street,
        StreetNumber = cardiogramModel.Call.StreetNumber,
        ApartmentNumber = cardiogramModel.Call.ApartmentNumber,
        Entrance = cardiogramModel.Call.Entrance,
        Priority = (int)cardiogramModel.Call.Priority,
        HospitalizationAddress = cardiogramModel.Call.HospitalizationAddress,
        CallType = cardiogramModel.Call.CallType,
        Reason = cardiogramModel.Call.Reason,
        ReceiveTime = cardiogramModel.Call.ReceiveTime,
        TransferTime = cardiogramModel.Call.TransferTime,
        DepartureTime = cardiogramModel.Call.DepartureTime,
        ArrivalTime = cardiogramModel.Call.ArrivalTime,
        CallDiagnosis = cardiogramModel.Call.CallDiagnosis
    };
}


}