namespace KpoApi.Application.Models.ResultModels;

public sealed record ActionErrorModel(
    string ErrorCode, 
    string ErrorMessage);