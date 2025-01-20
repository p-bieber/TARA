using TARA.AuthenticationService.Domain.Users;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserReadRepository
{
    Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result<User>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<User>> GetUserByNameAsync(string username, CancellationToken cancellationToken);
}
