using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.Data;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;
using KpoApi.Domain.Enums;
using Filter = KpoApi.Application.Models.Data.Filter;

namespace KpoApi.Application.Services;

public class CardiogramService : ICardiogramService, ISaveEntitiesService
{
    private readonly IPostgresProvider _postgresProvider;
    
    public CardiogramService(IPostgresProvider postgresProvider, IPostgresEfCoreProvider efCoreProvider)
    {
        _postgresProvider = postgresProvider;
    }
    
    public async Task<EntireCardiogramModel?> GetCardiogram(Guid guid)
    {
        var entireModel = await _postgresProvider.GetCardiogram(guid, CancellationToken.None);

        return entireModel;
    }

    public Task<Cardiogram> SendCardiogram(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeCardiogramState(Guid guid, CardiogramState cardiogramState)
    {
        var isChanged = _postgresProvider.ChangeCardiogramState(guid, (int)cardiogramState, CancellationToken.None);

        return isChanged;
    }

    public async Task<ResponseModel<CardiogramModel[]>> GetCardiograms(Filter filter)
    {
        if (string.IsNullOrWhiteSpace(filter.DateFrom.ToString()))
        {
            filter.DateFrom = DateTime.MinValue;
        }
        
        if (string.IsNullOrWhiteSpace(filter.DateTo.ToString()))
        {
            filter.DateTo = DateTime.Now;
        }
        var cardiograms = await _postgresProvider.GetCardiogramsByFilter(filter, CancellationToken.None);

        return new ResponseModel<CardiogramModel[]> (cardiograms,
            true,
            null);
    }

    public async Task<ResponseModel<Organization[]>> GetOrganizations()
    {
        try
        {
            var organizationArray = await _postgresProvider.GetOrganizations(CancellationToken.None);

            if (organizationArray.Length > 0)
            {
                return new ResponseModel<Organization[]>(
                    IsSuccess: true,
                    SuccessEntity: organizationArray,
                    ErrorEntity: null);
            }
            return new ResponseModel<Organization[]>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Не удалось получить организации"));
            
        }
        catch (Exception e)
        {
            return new ResponseModel<Organization[]>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Непредвиденная ошибка при получении оргинизаций"));
        }
        
    }

    public Task<User[]> GetUsers(Guid organizationGuid)
    {
        var usersArray = _postgresProvider.GetUsers(organizationGuid ,CancellationToken.None);

        return usersArray;
    }

    public async Task<ResponseModel<Cardiograph[]>> GetCardiographs(Guid? userGuid)
    {
        try
        {
            var cardiographs = await _postgresProvider.GetCardiographs(userGuid ,CancellationToken.None);
            
            if (cardiographs.Length > 0)
            {
                return new ResponseModel<Cardiograph[]>(
                    IsSuccess: true,
                    SuccessEntity: cardiographs,
                    ErrorEntity: null);
            }
            else
            {
                return new ResponseModel<Cardiograph[]>(
                    IsSuccess: false,
                    SuccessEntity: null,
                    ErrorEntity: new ActionErrorModel("400", "Не удалось получить кардиографы"));
            }
           
        }
        catch (Exception e)
        {
             return new ResponseModel<Cardiograph[]>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Непредвиденная ошибка при получении кардиографов"));
        }
    }

    public Task<Cardiogram[]> GetCardiograms(string serialNumber)
    {
        var cardiograms = _postgresProvider.GetCardiograms(serialNumber ,CancellationToken.None);

        return cardiograms;
    }

    public Task<User[]> GetUsersByCardiograms(Guid cardiogramUuid)
    {
        var cardiograms = _postgresProvider.GetUsersByCardiograms(cardiogramUuid ,CancellationToken.None);

        return cardiograms;
    }

    public async Task<ResponseModel<User>> SaveUser(User newUser)
    {
        if (newUser.Login.Length < 4)
        {
            return new ResponseModel<User>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Табельный номер не может быть менее 4 символов"));
        }
        if (!string.IsNullOrWhiteSpace(newUser.PhoneNumber.ToString()!) 
            && !(newUser.PhoneNumber.ToString()!.Length >= 10 || newUser.PhoneNumber.ToString()!.Length <= 11))
        {
            return new ResponseModel<User>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Номер телефона должен содержать 10-11 цифр"));
        }
        
        var user = await _postgresProvider.SaveUser(newUser ,CancellationToken.None);

        return new ResponseModel<User>(
            IsSuccess: true,
            SuccessEntity: user,
            ErrorEntity: null);
    }
    
    public async Task<ResponseModel<Cardiogram>> SaveCardiogram(Cardiogram newCardiogram)
    {
        if (newCardiogram.MeasurementTime > newCardiogram.ReceivedTime)
        {
            return new ResponseModel<Cardiogram>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Время измерения не может быть меньше времени получения"));
        }
        
        var cardiogram = await _postgresProvider.SaveCardiogram(newCardiogram ,CancellationToken.None);

        return new ResponseModel<Cardiogram>(
            IsSuccess: true,
            SuccessEntity: cardiogram,
            ErrorEntity: null);
    }
    
    public async Task<ResponseModel<Call>> SaveCall(Call newCall)
    {
        if (string.IsNullOrWhiteSpace(newCall.CallType))
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Тип вызова не может быть пустым"));
        }
        
        if (string.IsNullOrWhiteSpace(newCall.Priority.ToString()) || (newCall.Priority < 0 || newCall.Priority > (Priority)9))
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Приоритет должен быть в диапазоне от 1 до 9"));
        }
        
        if (string.IsNullOrWhiteSpace(newCall.CallDiagnosis))
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Диагноз не может быть пустым"));
        }
        
        if (newCall.PatientAge > 150 || newCall.PatientAge < 0)
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Возраст пациента должен быть в пределах от 0 до 180"));
        }
        
        if (newCall.ReceiveTime > newCall.TransferTime)
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Время получения не может быть больше времени передачи"));
        }
        if (newCall.TransferTime > newCall.DepartureTime)
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Время передачи не может быть больше времени отправления"));
        }
        if (newCall.DepartureTime > newCall.ArrivalTime)
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Время отправления не может быть больше времени прибытия"));
        }
        
        if (string.IsNullOrWhiteSpace(newCall.Reason))
        {
            return new ResponseModel<Call>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Причина вызова не может быть пустой"));
        }
        
        var call = await _postgresProvider.SaveCall(newCall ,CancellationToken.None);

        return new ResponseModel<Call>(
            IsSuccess: true,
            SuccessEntity: call,
            ErrorEntity: null);;
    }
    
    public async Task<ResponseModel<Cardiograph>> SaveCardiograph(Cardiograph newCardiograph)
    {
        if (newCardiograph.SerialNumber!.Length < 3)
        {
            return new ResponseModel<Cardiograph>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Серийный номер кардиографа не может быть менее 3 символов"));
        }
        if (string.IsNullOrWhiteSpace(newCardiograph.CardiographName))
        {
            return new ResponseModel<Cardiograph>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Название кардиографа не может быть пустой"));
        }
        if (string.IsNullOrWhiteSpace(newCardiograph.ManufacturerName))
        {
            return new ResponseModel<Cardiograph>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Название компании не может быть пустой"));
        }
        
        
        var cardiograph = await _postgresProvider.SaveCardiograph(newCardiograph ,CancellationToken.None);

        return new ResponseModel<Cardiograph>(
            IsSuccess: true,
            SuccessEntity: cardiograph,
            ErrorEntity: null);
    }
    
    public async Task<ResponseModel<ResultsCardiogram>> SaveResult(ResultsCardiogram newCardiogram)
    {
        if (newCardiogram.DiagnosisMain!.Length < 5)
        {
            return new ResponseModel<ResultsCardiogram>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Диагноз не может быть менее 5 символов"));
        }
        if (newCardiogram.Description!.Length < 5)
        {
            return new ResponseModel<ResultsCardiogram>(
                IsSuccess: false,
                SuccessEntity: null,
                ErrorEntity: new ActionErrorModel("400", "Описание не может быть менее 5 символов"));
        }

        var result = await _postgresProvider.SaveResult(newCardiogram ,CancellationToken.None);
        
        return new ResponseModel<ResultsCardiogram>(
            IsSuccess: true,
            SuccessEntity: result,
            ErrorEntity: null);
    }

    public async Task<ResponseModel<Call[]>> GetCalls()
    {
        var calls = await _postgresProvider.GetCalls(CancellationToken.None);

        return new ResponseModel<Call[]>(
            IsSuccess: true,
            SuccessEntity: calls,
            ErrorEntity: null);

    }
}