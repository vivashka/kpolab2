namespace KpoApi.Application.Models.ResultModels;

public sealed record ResponseModel<T>(
    T? SuccessEntity, 
    bool IsSuccess,
    ActionErrorModel? ErrorEntity
    );