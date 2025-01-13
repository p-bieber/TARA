using FluentValidation;
using TARA.AuthenticationService.Application.Validators;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Application.Factories;
public class EmailFactory
{
    private readonly EmailValidator _validator = new();
    public Email Create(string address)
    {
        var email = Email.Create(address);
        var result = _validator.Validate(email);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors[0].ErrorMessage, result.Errors);
        }
        return email;
    }
}