using TARA.AuthenticationService.Domain.Users;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Interfaces;

public interface IUserWriteRepository
{
    Task<Result> AddUserAsync(User user);
    Task<Result> DeleteUserAsync(string id);
    Task<Result> UpateUserAsync(User updatedUser);
}