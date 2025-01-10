using FluentValidation;
using TARA.AuthenticationService.Application.Validators;
using TARA.AuthenticationService.Domain.ValueObjects;
using TARA.AuthenticationService.Infrastructure.Services;

namespace TARA.AuthenticationService.Application.Factories;

public class PasswordFactory
{
    private readonly PasswordValidator _validator;
    private readonly PasswordHasher _passwordHasher;

    public PasswordFactory(PasswordValidator validator, PasswordHasher passwordHasher)
    {
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public Password Create(string password)
    {
        var unsecurePassword = Password.Create(password);
        var result = _validator.Validate(unsecurePassword);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors[0].ErrorMessage, result.Errors);
        }

        var securePassword = _passwordHasher.HashPassword(unsecurePassword.Value);
        return Password.Create(securePassword);
    }
}
