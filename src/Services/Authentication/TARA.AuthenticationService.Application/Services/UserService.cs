using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, PasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    public async Task<(bool, UserId?)> ValidateUserAsync(string userName, string password)
    {
        var id = await _userRepository.GetUserIdAsync(userName);
        if (id == null)
            return (false, null);
        var user = await _userRepository.GetByIdAsync(id);
        return (user is not null && user.Password.Value.Equals(password), user?.Id);
    }
}
