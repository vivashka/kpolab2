using KpoApi.Application.Models.ResultModels;
using KpoApi.Domain.Entities;

namespace KpoApi.Application.Contracts;

public interface IUserService
{
    Task<ResponseModel<User>> CreateUser(User user, string CardiographId);
    
    Task<ResponseModel<User>> UserAuthentication(string login, string password);

    Task<bool> DeleteUser(User user, bool isAdmin);

    Task<User[]> GetUsers();
}