using System.Security.Cryptography;
using System.Text;
using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;

namespace KpoApi.Application.Services;

public class UserService : IUserService
{
    private readonly IPostgresEfCoreProvider _efCoreProvider;

    public UserService(IPostgresEfCoreProvider efCoreProvider)
    {
        _efCoreProvider = efCoreProvider;
    }

    public async Task<ResponseModel<User>> CreateUser(User user, string CardiographId)
    {
        using (var md5 = MD5.Create())
        {
            var inputBytes = Encoding.UTF8.GetBytes(user.Password + user.Login);
            var hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            user.Password =  sb.ToString();
        }

        var response = await _efCoreProvider.CreateUser(user, CardiographId);

        if (response is {})
        {
            return new ResponseModel<User>(
                IsSuccess: true,
                SuccessEntity: response,
                ErrorEntity: null);
        }
        return new ResponseModel<User>(
            IsSuccess: false,
            SuccessEntity: null,
            ErrorEntity: new ActionErrorModel("400", "Не удалось создать пользователя"));
        
    }

    public async Task<ResponseModel<User>> UserAuthentication(string login, string password)
    {
        var response = await _efCoreProvider.UserAuthentication(login, password);
        if (response is {})
        {
            return new ResponseModel<User>(
                IsSuccess: true,
                SuccessEntity: response,
                ErrorEntity: null);
        }

        return new ResponseModel<User>(
            IsSuccess: false,
            SuccessEntity: response,
            ErrorEntity: new ActionErrorModel("400", 
                "Не удалось найти пользователя с таким логином и паролем"));
    }

    public async Task<bool> DeleteUser(User user, bool isAdmin) // TODO как будет время нужно возвращать модель ответа 
    {
        if (isAdmin)
        {
            return await _efCoreProvider.DeleteUser(user); //TODO нужно удалять fk на таблину organization 
        }

        return false;
    }

    public async Task<User[]> GetUsers()
    {
        return await _efCoreProvider.GetUsers();
    }
}