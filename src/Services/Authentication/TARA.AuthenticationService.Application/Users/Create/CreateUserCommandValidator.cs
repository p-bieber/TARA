using FluentValidation;
using TARA.AuthenticationService.Domain.Users.ValueObjects;

namespace TARA.AuthenticationService.Application.Users.Create;
internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull().NotEmpty()
            .MinimumLength(Username.MinLength)
            .MaximumLength(Username.MaxLength)
            .Matches("^[A-Za-z][A-Za-z0-9]*$");

        RuleFor(x => x.Password)
            .NotNull().NotEmpty()
            .MinimumLength(8)
            .MaximumLength(25)
            .Matches(@"[A-Z]")
            .Matches(@"[a-z]")
            .Matches(@"\d")
            .Matches(@"[\W_]");

        RuleFor(x => x.Email)
            .NotNull().NotEmpty()
            .EmailAddress(mode: FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
    }
}
