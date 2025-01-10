using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
