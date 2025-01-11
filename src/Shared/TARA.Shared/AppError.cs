namespace TARA.Shared;
public sealed record AppError(string Code, string Message) : IEquatable<AppError>
{
    public static readonly AppError None = new(string.Empty, string.Empty);
    public static readonly AppError NullValue = new("Error.NullValue", "Null value was provided");
    public static readonly AppError NotFound = new("Error.NotFound", "Object not found");

    public static implicit operator Result(AppError error) => Result.Failure(error);

    public Result ToResult() => Result.Failure(this);
}
