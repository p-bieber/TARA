using TARA.AuthenticationService.Domain.Entities;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<string?> GetUserIdAsync(string username);
    Task AddUserAsync(User user);
    Task UpateUserAsync(User updatedUser);
    Task DeleteUserAsync(string id);
}
