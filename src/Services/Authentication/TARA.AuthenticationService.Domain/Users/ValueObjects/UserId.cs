using TARA.Shared.Primitives;
using TARA.Shared.ResultObject;

namespace TARA.AuthenticationService.Domain.Users.ValueObjects;
public class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }
    public static Result<UserId> Create()
    {
        return new UserId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
