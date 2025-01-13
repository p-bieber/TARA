using FluentValidation;
using TARA.AuthenticationService.Application.Validators;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Application.Factories;

public class UserNameFactory
{
    private readonly UserNameValidator _validator = new();

    public Username Create(string name)
    {
        var username = Username.Create(name);
        var result = _validator.Validate(username);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors[0].ErrorMessage, result.Errors);
        }
        return username;
    }
}
