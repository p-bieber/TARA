using FluentValidation;
using TARA.AuthenticationService.Application.Validators;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Application.Factories;

public class UserNameFactory
{
    private readonly UserNameValidator _validator;

    public UserNameFactory(UserNameValidator validator)
    {
        _validator = validator;
    }

    public UserName Create(string name)
    {
        var username = UserName.Create(name);
        var result = _validator.Validate(username);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors[0].ErrorMessage, result.Errors);
        }
        return username;
    }
}
