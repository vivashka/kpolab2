using System.Security.Cryptography;
using System.Text;
using KpoApi.Application.Contracts;
using KpoApi.Application.Contracts.External;
using KpoApi.Domain.Entities;

namespace KpoApi.Application.Services;

public class UserService : IUserService
{
    
    private readonly IPostgresEfCoreProvider _efCoreProvider;

    public UserService(IPostgresEfCoreProvider efCoreProvider)
    {
        _efCoreProvider = efCoreProvider;
    }

    public Task<User> CreateUser(User user)
    {
        var sha1 = new SHA1Managed();
        var plaintextBytes = Encoding.UTF8.GetBytes(user.Password + user.Login);
        var hashBytes = sha1.ComputeHash(plaintextBytes);
        
        var hashPassword = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            hashPassword.AppendFormat("{0:x2}", hashByte);
        }

        user.Password = hashPassword.ToString();
        
        var response = _efCoreProvider.CreateUser(user);
        
        return response;
    }

    public Task<bool> UserAuthentication(string login, string password)
    {
        throw new NotImplementedException();
    }
}