using TARA.AuthenticationService.Domain.Entities;
using TARA.Shared;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserRepository
{
    Task<Result<User>> GetUserByIdAsync(string id);
    Task<Result<User>> GetUserByNameAsync(string username);
    Task<Result> AddUserAsync(User user);
    Task<Result> UpateUserAsync(User updatedUser);
    Task<Result> DeleteUserAsync(string id);
}
