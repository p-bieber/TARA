using FluentValidation;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Application.Validators;

public class UserNameValidator : AbstractValidator<UserName>
{
    public UserNameValidator()
    {
        RuleFor(x => x.Value)
            .MinimumLength(5).WithMessage("Username must consist of at least 5 characters")
            .MaximumLength(15).WithMessage("Username is too long. maximum 15 characters allowed")
            .Matches("^[A-Za-z][A-Za-z0-9]*$").WithMessage("Username must not contain any special characters")
            ;
    }
}
