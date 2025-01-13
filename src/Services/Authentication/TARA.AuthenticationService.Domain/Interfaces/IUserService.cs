using TARA.AuthenticationService.Domain.Entities;
using TARA.Shared;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserService
{
    Task<Result> CreateUser(string username, string password, string email);
    Task<Result<User>> GetUserByNameAsync(string id);
    Task<Result<User>> VerifyUserPasswordAsync(string username, string password);
}