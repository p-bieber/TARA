using TARA.AuthenticationService.Domain.Users;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Interfaces;

public interface IUserWriteRepository
{
    Task<Result> AddUserAsync(User user, CancellationToken cancellationToken);
    Task<Result> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
    Task<Result> UpateUserAsync(User updatedUser, CancellationToken cancellationToken);
}