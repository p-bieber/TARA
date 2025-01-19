namespace TARA.Shared.ResultObject;
public interface IValidationResult
{
    public static readonly Error ValidationError = new("ValidationError", "Validation not succeeded");
    Error[] Errors { get; }
}
