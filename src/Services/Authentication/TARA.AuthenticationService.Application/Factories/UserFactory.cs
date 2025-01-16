﻿using TARA.AuthenticationService.Domain.Entities;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Application.Factories;

public class UserFactory(IPasswordHasher passwordHasher)
{
    private readonly UserNameFactory _userNameFactory = new();
    private readonly PasswordFactory _passwordFactory = new(passwordHasher);
    private readonly EmailFactory _emailFactory = new();

    public Task<User> Create(string username, string password, string email)
    {
        // Validation
        var _username = _userNameFactory.Create(username);
        var _password = _passwordFactory.Create(password);
        var _email = _emailFactory.Create(email);

        var user = User.Create(_username, _password, _email);
        return Task.FromResult(user);
    }
}