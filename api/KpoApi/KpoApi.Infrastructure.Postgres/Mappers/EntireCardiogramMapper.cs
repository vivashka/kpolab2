using KpoApi.Application.Models.Data;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;

namespace KpoApi.Infrastructure.PostgresEfCore.Mappers;

public class EntireCardiogramMapper : IEntireCardiogramMapper
{
    public EntireCardiogramModel MapOrderEntityToModel(EntireCardiogramEntity cardiogramEntity)
    {
        EntireCardiogramModel entireCardiogramModel = new EntireCardiogramModel
        {
            CardiogramUuid = cardiogramEntity.CardiogramUuid,
            ReceivedTime = cardiogramEntity.ReceivedTime,
            MeasurementTime = cardiogramEntity.MeasurementTime,
            CardiogramState = (CardiogramState)cardiogramEntity.CardiogramState,
            RawCardiogram = cardiogramEntity.RawCardiogram,
            
            
            Result = new ResultCardiogramModel
            {
                Description = cardiogramEntity.Description,
                DiagnosisMain = cardiogramEntity.DiagnosisMain,
                ResultCardiogramUuid = cardiogramEntity.ResultCardiogramUuid
            },
            
            Call = new CallDataModel
            {
                CallUuid = cardiogramEntity.CallUuid,
                CallState = cardiogramEntity.CallState,
                CallType = cardiogramEntity.CallType,
                CallDiagnosis = cardiogramEntity.CallDiagnosis,
    
                PatientName = cardiogramEntity.PatientName,
                PatientSurname = cardiogramEntity.PatientSurname,
                PatientPatronymic = cardiogramEntity.PatientPatronymic,
                PatientAge = cardiogramEntity.PatientAge,
                PatientSex = cardiogramEntity.PatientSex,
                PatientId = cardiogramEntity.PatientId,
    
                BrigadeNumber = cardiogramEntity.BrigadeNumber,
                BrigadeProfile = cardiogramEntity.BrigadeProfile,
                MainChiefNumber = cardiogramEntity.MainChiefNumber,
                MainChiefFullName = cardiogramEntity.MainChiefFullName,
                
                
                YearNumber = cardiogramEntity.YearNumber,
                DayNumber = cardiogramEntity.DayNumber,
                RegionCode = cardiogramEntity.RegionCode,
                City = cardiogramEntity.City,
                SsmpNumber = cardiogramEntity.SsmpNumber,
                Street = cardiogramEntity.Street,
                StreetNumber = cardiogramEntity.StreetNumber,
                ApartmentNumber = cardiogramEntity.ApartmentNumber,
                Entrance = cardiogramEntity.Entrance,
                Priority = (Priority)cardiogramEntity.Priority,
                HospitalizationAddress = cardiogramEntity.HospitalizationAddress,
                HospitalizationDistance = cardiogramEntity.HospitalizationDistance,
    
                Reason = cardiogramEntity.Reason,
                ReceiveTime = cardiogramEntity.ReceiveTime,
                TransferTime = cardiogramEntity.TransferTime,
                DepartureTime = cardiogramEntity.DepartureTime,
                ArrivalTime = cardiogramEntity.ArrivalTime
            },
            Cardiograph = new Cardiograph()
            {
                CardiographName = cardiogramEntity.CardiographName,
                SerialNumber = cardiogramEntity.CardiographUuid,
                ManufacturerName = cardiogramEntity.ManufacturerName
            }
        };

        return entireCardiogramModel;
    }

    public EntireCardiogramEntity MapOrderModelToEntity(EntireCardiogramModel cardiogramEntity)
    {
        throw new NotImplementedException();
    }
}