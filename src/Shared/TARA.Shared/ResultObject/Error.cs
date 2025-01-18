namespace TARA.Shared.ResultObject;
public sealed record Error(string Code, string Message) : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
    public static readonly Error NotFound = new("Error.NotFound", "Object not found");

    public static implicit operator Result(Error error) => Result.Failure(error);
    public Result ToResult() => Result.Failure(this);
}
