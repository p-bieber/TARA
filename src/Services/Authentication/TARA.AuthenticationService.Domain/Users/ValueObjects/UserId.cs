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
    public static Result<UserId> Create(Guid? guid = null)
    {
        if (guid is null)
        {
            guid = Guid.NewGuid();
        }
        return new UserId((Guid)guid);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
