using TARA.AuthenticationService.Domain.Entities;

namespace TARA.AuthenticationService.Domain.Interfaces;
public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByNameAsync(string username);
    Task<string?> GetUserIdAsync(string username);
    Task AddUserAsync(User user);
    Task UpateUserAsync(User updatedUser);
    Task DeleteUserAsync(string id);
}
