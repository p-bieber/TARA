using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Application.Factories;

public class UserFactory
{
    private readonly UserNameFactory _userNameFactory;
    private readonly PasswordFactory _passwordFactory;
    private readonly EmailFactory _emailFactory;
    private readonly IPasswordHasher _passwordHasher;

    public UserFactory(UserNameFactory userNameFactory, PasswordFactory passwordFactory, EmailFactory emailFactory, IPasswordHasher passwordHasher)
    {
        _userNameFactory = userNameFactory;
        _passwordFactory = passwordFactory;
        _emailFactory = emailFactory;
        _passwordHasher = passwordHasher;
    }

    public User Create(string username, string password, string email)
    {
        // Validation
        var _username = _userNameFactory.Create(username);
        var _password = _passwordFactory.Create(password);
        var _email = _emailFactory.Create(email);

        //PasswordHashing
        var sercurePassword = Password.Create(_passwordHasher.HashPassword(_password.Value));

        return User.Create(_username, sercurePassword, _email);
    }
}