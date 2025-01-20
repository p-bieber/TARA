using FluentValidation;
using TARA.AuthenticationService.Domain.Users.Errors;
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
            .Matches(@"[A-Z]").WithMessage(PasswordErrors.NoUpper.Message)
            .Matches(@"[a-z]").WithMessage(PasswordErrors.NoLower.Message)
            .Matches(@"\d").WithMessage(PasswordErrors.NoDigit.Message)
            .Matches(@"[\W_]").WithMessage(PasswordErrors.NoSymbol.Message);

        RuleFor(x => x.Email)
            .NotNull().NotEmpty()
            .EmailAddress(mode: FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
    }
}
