using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserFactory _userFactory;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, UserFactory userFactory, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _userFactory = userFactory;
        _passwordHasher = passwordHasher;
    }
    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    public async Task<(bool, UserId?)> ValidateUserAsync(string username, string password)
    {
        var id = await _userRepository.GetUserIdAsync(username);
        if (id == null)
            return (false, null);
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return (false, null);
        var isPasswordVerified = _passwordHasher.VerifyPassword(password, user.Password.Value);
        if (!isPasswordVerified)
        {
            return (false, null);
        }
        return (true, user.Id);
    }

    public Task CreateUser(string username, string password, string email)
    {
        var user = _userFactory.Create(username, password, email);

        return Task.CompletedTask;
    }
}
