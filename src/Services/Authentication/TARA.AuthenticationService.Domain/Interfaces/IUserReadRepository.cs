using TARA.AuthenticationService.Domain.Users;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserReadRepository
{
    Task<Result<User>> GetUserByIdAsync(string id);
    Task<Result<User>> GetUserByNameAsync(string username);
}
