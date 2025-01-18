using FluentValidation;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Application.Validators;

public class UserNameValidator : AbstractValidator<Username>
{
    public UserNameValidator()
    {
        RuleFor(x => x.Value)
            .MinimumLength(Username.MinLength).WithMessage("Username must consist of at least 5 characters")
            .MaximumLength(Username.MaxLength).WithMessage("Username is too long. maximum 15 characters allowed")
            .Matches("^[A-Za-z][A-Za-z0-9]*$").WithMessage("Username must not contain any special characters")
            ;
    }
}
