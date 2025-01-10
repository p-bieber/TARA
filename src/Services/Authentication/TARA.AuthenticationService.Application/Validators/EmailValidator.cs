using FluentValidation;
using TARA.AuthenticationService.Domain.ValueObjects;

namespace TARA.AuthenticationService.Application.Validators;
public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(x => x.Value)
            .NotNull().NotEmpty()
            .EmailAddress(mode: FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            ;
    }
}
