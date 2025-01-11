using TARA.AuthenticationService.Application.Factories;
using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.Interfaces;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserFactory _userFactory;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;

        _userFactory = new(passwordHasher);
    }
    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }
    public async Task<(bool, UserId?)> ValidateUserAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByNameAsync(username);
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
